const path = require('path');
const fs = require('fs');

const entryPoints = fs
  .readdirSync(path.resolve('entry'))
  .map(file => path.resolve('entry', file));

require('esbuild').build({
  entryPoints,
  bundle: true,
  outdir: 'build',
  minify: true
}).catch(() => process.exit(1));