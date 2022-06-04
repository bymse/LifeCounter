import path from 'path';
import fs from 'fs/promises'
import signalrPackage from '@microsoft/signalr/package.json' 

const signalrBasePath = path.resolve('node_modules', '@microsoft', 'signalr', 'dist', 'browser');

export default function copySignalr(buildFolder) {
  console.log('Copying @microsoft/signalr to dist');
  const version = signalrPackage.version.replace(/\./g, '_');
  return copyFile(`signalr.min.js`, `signalr.${version}.js`, buildFolder);
}

function copyFile(file, targetFile, buildFolder) {
  const src = path.join(signalrBasePath, file);
  const target = path.resolve('build', buildFolder, targetFile);
  return fs.copyFile(src, target);
}