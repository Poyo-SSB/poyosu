var document = app.activeDocument;

function exportFile(name, width, height, multiplier) {
    var exportOptions = new ExportOptionsPNG24();
    exportOptions.matte = false;
    exportOptions.artBoardClipping = true;
    exportOptions.horizontalScale = 100 * width / document.width * multiplier;
    exportOptions.verticalScale = 100 * height / document.height * multiplier;
    
    var path = document.path + "/" + name + ".png"
    
    document.exportFile(new File(path), ExportType.PNG24, exportOptions);
}

var textItem = document.textFrames.getByName("Text");

function process(text, name) {
    textItem.contents = text;
    const buffer = 2.5;
    document.artboards[0].artboardRect = [-buffer, 0, textItem.width + buffer, -28]
    exportFile("scoreentry-" + name + "@2x", textItem.width + 2 * buffer, 28, 0.9);
    exportFile("scoreentry-" + name, (textItem.width + 2 * buffer) / 2, 14, 0.45);
}

for (var i = 0; i < 10; i++) {
    process(i, i)
}
process(",", "comma")
process(".", "dot")
process("%", "percent")
process("x", "x")

// This is unused.