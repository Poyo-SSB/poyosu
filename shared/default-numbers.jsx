var document = app.activeDocument;

function exportFile(name, width, height, multiplier) {
    var exportOptions = new ExportOptionsPNG24();
    exportOptions.matte = false;
    exportOptions.artBoardClipping = true;
    exportOptions.horizontalScale = (width / document.width) * 100 * multiplier;
    exportOptions.verticalScale = (height / document.height) * 100 * multiplier;
    
    var path = document.path + "/" + name + ".png"
    
    document.exportFile(new File(path), ExportType.PNG24, exportOptions);
}

var textItem = document.textFrames.getByName("Text");

function process(text) {
    textItem.contents = text;
    document.artboards[0].artboardRect = [0, 0, textItem.width, -100]
    exportFile("default-" + text + "@2x", textItem.width, 100, 0.95);
    exportFile("default-" + text, textItem.width / 2, 50, 0.475);
}

for (var i = 0; i < 10; i++) {
    process(i)
}