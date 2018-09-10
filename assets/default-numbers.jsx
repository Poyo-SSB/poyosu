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

function process(text) {
    textItem.contents = text;
    document.artboards[0].artboardRect = [0, 0, textItem.width, -100]
    exportFile("../shared/default-" + text + "@2x", textItem.width, 100, 0.95);
}

for (var i = 0; i < 10; i++) {
    process(i)
}