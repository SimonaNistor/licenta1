var FS = require('fs');
var JSON5 = require('./json5');

// Modeled off of (v0.6.18 link; check latest too):
// https://github.com/joyent/node/blob/v0.6.18/lib/module.js#L468-L472
require.extensions['.json5'] = function (module, filename) {
    var content = FS.readFileSync(filename, 'utf8');
    module.exports = JSON5.parse(content);
};