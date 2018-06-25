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

var sliderBallWidth = 12.5; //px

var overlay = document.layers.getByName("Overlay");
var overlayCircle = document.pathItems.getByName("Overlay Circle");
var standard = document.layers.getByName("Standard");
var lite = document.layers.getByName("Lite");
var classic = document.layers.getByName("Classic");

overlay.visible = false;
standard.visible = false;
lite.visible = false;
classic.visible = true;

exportFile("../classic/hitcircle@2x", 288, 288);
exportFile("../classic/hitcircle", 144, 144);

overlay.visible = false;
standard.visible = false;
lite.visible = true;
classic.visible = false;

exportFile("../lite/hitcircle@2x", 288, 288);
exportFile("../lite/hitcircle", 144, 144);

overlay.visible = false;
standard.visible = true;
lite.visible = false;
classic.visible = false;

exportFile("../standard/hitcircle@2x", 288, 288);
exportFile("../standard/hitcircle", 144, 144);

overlay.visible = true;
standard.visible = false;
lite.visible = false;
classic.visible = false;

overlayCircle.strokeWidth = sliderBallWidth;

exportFile("../standard+lite/sliderb0@2x", 288, 288);
exportFile("../standard+lite/sliderb0", 144, 144);

overlayCircle.strokeWidth = 15;

exportFile("../shared/hitcircleoverlay@2x", 288, 288);
exportFile("../shared/hitcircleoverlay", 144, 144);

overlay.visible = true;
standard.visible = true;
lite.visible = false;
classic.visible = false;