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
    exportOptions.horizontalScale = 100 * width / document.width;
    exportOptions.verticalScale = 100 * height / document.height;
    
    var path = document.path + "/" + name + ".png"
    
    document.exportFile(new File(path), ExportType.PNG24, exportOptions);
}

var standardHitCircleOpacity = 80; //%
var liteHitCircleOpacity = 55; //%
var liteSliderBallWidth = 12.5; //px

var overlay = document.layers.getByName("Overlay");
var overlayCircle = document.pathItems.getByName("Overlay Circle");
var overlayGlow = document.pathItems.getByName("Overlay Glow");
var hitCircle = document.layers.getByName("Hit Circle");
var circle = document.pathItems.getByName("Circle");

overlay.visible = false;
overlayGlow.hidden = true;
hitCircle.visible = true;

circle.fillColor = colorFromHex("#000000");
circle.opacity = liteHitCircleOpacity;

exportFile("../lite/hitcircle@2x", 288, 288);
exportFile("../lite/hitcircle", 144, 144);

overlayGlow.hidden = false;

circle.fillColor = colorFromHex("#ffffff");
circle.opacity = standardHitCircleOpacity;

exportFile("../standard/hitcircle@2x", 288, 288);
exportFile("../standard/hitcircle", 144, 144);

overlay.visible = true;
hitCircle.visible = false;

overlayCircle.strokeWidth = liteSliderBallWidth;

exportFile("../lite/sliderb0@2x", 288, 288);
exportFile("../lite/sliderb0", 144, 144);

overlayCircle.strokeWidth = 15;

exportFile("hitcircleoverlay@2x", 288, 288);
exportFile("hitcircleoverlay", 144, 144);

overlay.visible = true;
hitCircle.visible = true;
overlayGlow.hidden = false;
hitCircle.visible = true;