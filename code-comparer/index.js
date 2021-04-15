const fs  = require("fs");
const sim = require("string-similarity");
const yml = require("yaml");
const csv = require('csv-writer');

const getDirCodes = dir => {
    const dirCodes = {};

    fs.readdirSync(dir).forEach(filename => {
        const path = `${dir}/${filename}`;

        if (fs.lstatSync(path).isFile()) {
            dirCodes[filename] = fs.readFileSync(path).toString();
        }
    });

    return dirCodes;
};

const [input, output] = [yml.parse(fs.readFileSync('data/input.yml', 'utf8')), []];

input.dirsToCompare.forEach(volume => {
    const source = volume.split(":")[0];
    const target = volume.split(":")[1];
    const sourcePath = `${input.sourceBaseDir}/${source}`;
    const targetPath = `${input.targetBaseDir}/${target}`;

    const [raw, final] = [getDirCodes(sourcePath), getDirCodes(targetPath)];
    
    const temp = Object.keys(raw)
        .filter(k => k in final)
        .map(k => ({
            file: `${target}/${k}`,
            lines1: raw[k].split(/\r\n|\r|\n/).length,
            lines2: final[k].split(/\r\n|\r|\n/).length,
            sim: (sim.compareTwoStrings(raw[k], final[k]) * 100).toFixed(2)
        }));

    output.push(...temp);
});

const writer = csv.createObjectCsvWriter({
    path: 'data/output.csv',
    header: [
        { id: 'file', title: 'Arquivo' },
        { id: 'lines1', title: 'Linhas geradas' },
        { id: 'lines2', title: 'Linhas cód. final'},
        { id: 'sim', title: 'Aproveitamento (%)' }
    ]
});

writer.writeRecords(output).then(() =>
    console.log('The CSV file was written successfully! ✅'));
