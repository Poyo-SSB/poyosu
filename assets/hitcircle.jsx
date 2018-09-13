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

var sliderBallWidth = 12.5; //px

var overlay = document.layers.getByName("Overlay");
var overlayCircle = document.pathItems.getByName("Overlay Circle");
var standard = document.layers.getByName("Standard");
var lite = document.layers.getByName("Lite");
var liteCircle = document.pathItems.getByName("Lite Circle");

overlay.visible = false;
standard.visible = false;
lite.visible = true;

liteCircle.fillColor = colorFromHex("#ffffff");

exportFile("../lite/fruit-apple@2x", 316, 316);
exportFile("../lite/fruit-apple", 158, 158);
exportFile("../lite/fruit-grapes@2x", 316, 316);
exportFile("../lite/fruit-grapes", 158, 158);
exportFile("../lite/fruit-orange@2x", 316, 316);
exportFile("../lite/fruit-orange", 158, 158);
exportFile("../lite/fruit-pear@2x", 316, 316);
exportFile("../lite/fruit-pear", 158, 158);
exportFile("../lite/fruit-bananas@2x", 316, 316);
exportFile("../lite/fruit-bananas", 158, 158);
exportFile("../lite/fruit-drop@2x", 316, 316);
exportFile("../lite/fruit-drop", 158, 158);

liteCircle.fillColor = colorFromHex("#000000");

exportFile("../lite/hitcircle@2x", 288, 288);
exportFile("../lite/hitcircle", 144, 144);

overlay.visible = false;
standard.visible = true;
lite.visible = false;

exportFile("../standard/fruit-apple@2x", 316, 316);
exportFile("../standard/fruit-grapes@2x", 316, 316);
exportFile("../standard/fruit-orange@2x", 316, 316);
exportFile("../standard/fruit-pear@2x", 316, 316);
exportFile("../standard/fruit-bananas@2x", 316, 316);
exportFile("../standard/fruit-drop@2x", 316, 316);

exportFile("../standard/hitcircle@2x", 288, 288);

overlay.visible = true;
standard.visible = false;
lite.visible = false;

overlayCircle.strokeWidth = sliderBallWidth;

exportFile("../standard+lite/sliderb0@2x", 288, 288);

overlayCircle.strokeWidth = 15;

exportFile("../shared/fruit-apple-overlay@2x", 316, 316);
exportFile("../shared/fruit-grapes-overlay@2x", 316, 316);
exportFile("../shared/fruit-orange-overlay@2x", 316, 316);
exportFile("../shared/fruit-pear-overlay@2x", 316, 316);
exportFile("../shared/fruit-bananas-overlay@2x", 316, 316);
exportFile("../shared/fruit-drop-overlay@2x", 316, 316);

exportFile("../shared/hitcircleoverlay@2x", 288, 288);

overlay.visible = true;
standard.visible = true;
lite.visible = false;