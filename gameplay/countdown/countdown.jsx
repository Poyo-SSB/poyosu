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

var artboard = document.artboards.getByName("Artboard");
var textItem = document.textFrames.getByName("Text");

function process(text, file, x, y) {
    textItem.contents = text;
    artboard.artboardRect = [-x / 2, y / 2, x / 2, -y / 2];
    exportFile(file + "@2x", x, y);
    exportFile(file, x / 2, y / 2);
}

process("3", "count3", 400, 500);
process("2", "count2", 400, 500);
process("1", "count1", 400, 500);
process("Go!", "go", 800, 500);
process("Ready?", "ready", 1700, 700);