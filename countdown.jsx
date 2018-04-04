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

function process(text, file) {
    textItem.contents = text;
    exportFile(file + "@2x", 800, 500);
    exportFile(file, 400, 250);
}

process("3", "count3");
process("2", "count2");
process("1", "count1");
process("Go!", "go");