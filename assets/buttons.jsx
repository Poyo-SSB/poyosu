﻿var document = app.activeDocument;

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
    exportOptions.horizontalScale = 100 * width / document.width;
    exportOptions.verticalScale = 100 * height / document.height;
    
    var path = document.path + "/" + name + ".png"
    
    document.exportFile(new File(path), ExportType.PNG24, exportOptions);
}

var background = document.pathItems.getByName("Background");
var textItem = document.textFrames.getByName("Text");

function process(hex, text, file) {
    background.fillColor = colorFromHex(hex);
    textItem.contents = text;
    exportFile("../shared/" + file + "@2x", 1000, 180);
    exportFile("../shared/" + file, 500, 90);
}

process("#e8193b", "BACK TO MENU", "pause-back");
process("#f69100", "RETRY", "pause-retry");
process("#a2c80c", "CONTINUE", "pause-continue");
process("#0078f2", "WATCH REPLAY", "pause-replay");