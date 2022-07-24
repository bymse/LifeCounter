import path from 'path';
import fs from 'fs'
import {sassPlugin} from 'esbuild-sass-plugin';
import copySignalr from './signalr-build.mjs'
import esbuild from 'esbuild'

const getFiles = (dir) => fs
  .readdirSync(dir)
  .map(file => path.resolve(dir, file))

const entryPoints = fs
  .readdirSync(path.resolve('entry'))
  .flatMap(dir => getFiles(path.resolve('entry', dir)));

fs.rmSync(path.resolve('build'), {recursive: true, force: true});

const watch = process.argv[2] === 'watch';

esbuild.build({
  entryPoints,
  bundle: true,
  outdir: 'build',
  minify: true,
  entryNames: watch ? '[dir]/[name].watch' : '[dir]/[name].[hash]',
  watch,
  plugins: [sassPlugin()]
})
  .then(() => copySignalr('monitor'))
  .then(() => copySignalr('widget'))
  .then(() => watch && console.log('Watching...'))
  .catch((e) => {
    console.error(e);
    process.exit(1);
  });

