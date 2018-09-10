var document = app.activeDocument;

function colorFromHex(hex) {
    var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    var color = new RGBColor();
    color.red = parseInt(result[1], 16);
    color.green = parseInt(result[2], 16);
    color.blue = parseInt(result[3], 16);
    return color;
}

function exportFile(name, width, height) {
    var exportOptions = new ExportOptionsPNG24();
    exportOptions.matte = false;
    exportOptions.artBoardClipping = true;
    exportOptions.horizontalScale = 100 * width / document.width;
    exportOptions.verticalScale = 100 * height / document.height;
    
    var path = document.path + "/" + name + ".png"
    
    document.exportFile(new File(path), ExportType.PNG24, exportOptions);
}

var artboard = document.artboards[document.artboards.getActiveArtboardIndex()];

var scorebar = document.pathItems.getByName("Scorebar");
var textItem = document.textFrames.getByName("Text");
var flag = document.rasterItems.getByName("Flag");

artboard.artboardRect = [-50, 10, 1114, -70];

scorebar.hidden = false;
textItem.hidden = false;
flag.hidden = false;

textItem.contents = "Poyo";

scorebar.filled = false;
scorebar.stroked = true;
scorebar.strokeWidth = 3;
scorebar.strokeColor = colorFromHex("#ffffff");

exportFile("../shared/scorebar-bg@2x", 1164, 80);

artboard.artboardRect = [-41, -23, 1115, -71];

textItem.hidden = true;
flag.hidden = true;

scorebar.filled = true;
scorebar.stroked = false;
scorebar.fillColor = colorFromHex("#ffffff");

exportFile("../shared/scorebar-colour@2x", 1156, 48);