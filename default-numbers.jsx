var document = app.activeDocument;

function exportFile(name, width, height) {
    var exportOptions = new ExportOptionsPNG24();
    exportOptions.matte = false;
    exportOptions.artBoardClipping = true;
    exportOptions.horizontalScale = (width / document.width) * 100;
    exportOptions.verticalScale = (height / document.height) * 100;
    
    var path = document.path + "/" + name + ".png"
    
    document.exportFile(new File(path), ExportType.PNG24, exportOptions);
}

var textItem = document.textFrames.getByName("Text");

function process(text) {
    textItem.contents = text;
    exportFile("default-" + text + "@2x", 90, 90);
    exportFile("default-" + text, 45, 45);
}

for (var i = 0; i < 10; i++) {
    process(i)
}