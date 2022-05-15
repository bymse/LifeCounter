const path = require('path');
const fs = require('fs/promises');
const signalrPackage = require('@microsoft/signalr/package.json') 

const signalrBasePath = path.join(__dirname, 'node_modules', '@microsoft', 'signalr', 'dist', 'browser');

function copySignalr(buildFolder) {
  console.log('Copying @microsoft/signalr to dist');
  const version = signalrPackage.version.replace(/\./g, '_');
  return copyFile(`signalr.min.js`, `signalr.${version}.js`, buildFolder);
}

function copyFile(file, targetFile, buildFolder) {
  const src = path.join(signalrBasePath, file);
  const target = path.join(__dirname, 'build', buildFolder, targetFile);
  return fs.copyFile(src, target);
}

module.exports = copySignalr;