const path = require('path');
const fs = require('fs');
const copySignalr = require('./signalr-build');

const getFiles = (dir) => fs
  .readdirSync(dir)
  .map(file => path.resolve(dir, file))

const entryPoints = fs
  .readdirSync(path.resolve('entry'))
  .flatMap(dir => getFiles(path.join(__dirname, 'entry', dir)));

fs.rmSync(path.join(__dirname, 'build'), {recursive: true, force: true});

const watch = process.argv[2] === 'watch';

require('esbuild').build({
  entryPoints,
  bundle: true,
  outdir: 'build',
  minify: true,
  entryNames: watch ? '[dir]/[name].watch' : '[dir]/[name].[hash]',
  watch
})
  .then(() => copySignalr('monitor'))
  .then(() => watch && console.log('Watching...'))
  .catch((e) => {
    console.error(e);
    process.exit(1);
  });

