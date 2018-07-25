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

var cursorSize = 128; //px
var cursorColor = "#ff7fb3";
var cursorGlowOpacity = 100; // %

var standardTrailSize = 12; //px
var standardTrailColor = "#ff7fb3";
var standardTrailOpacity = 50; // %
var standardTrailCurveShallowness = 75; //%

var liteTrailSize = 48; //px
var liteTrailColor = "#ffffff";
var liteTrailOpacity = 45; // %
var liteTrailCurveShallowness = 50; //%

var cursor = document.layers.getByName("Cursor");
var ring = document.pathItems.getByName("Ring");
var innerGlow = document.pathItems.getByName("Inner Glow");
var glow = document.pathItems.getByName("Glow");
var trail = document.pathItems.getByName("Trail");

cursor.visible = false;
trail.hidden = true;

exportFile("../standard+classic/cursormiddle", 1, 1);

cursor.visible = false;
trail.hidden = false;

trail.fillColor.gradient.gradientStops[0].color = colorFromHex(standardTrailColor);
trail.fillColor.gradient.gradientStops[1].color = colorFromHex(standardTrailColor);
trail.fillColor.gradient.gradientStops[2].color = colorFromHex(standardTrailColor);
trail.fillColor.gradient.gradientStops[1].rampPoint = standardTrailCurveShallowness;
trail.opacity = standardTrailOpacity;
exportFile("../standard+classic/cursortrail@2x", standardTrailSize, standardTrailSize);
exportFile("../standard+classic/cursortrail", standardTrailSize / 2, standardTrailSize / 2);

trail.fillColor.gradient.gradientStops[0].color = colorFromHex(liteTrailColor);
trail.fillColor.gradient.gradientStops[1].color = colorFromHex(liteTrailColor);
trail.fillColor.gradient.gradientStops[2].color = colorFromHex(liteTrailColor);
trail.fillColor.gradient.gradientStops[1].rampPoint = liteTrailCurveShallowness;
trail.opacity = liteTrailOpacity;
exportFile("../lite/cursortrail@2x", liteTrailSize, liteTrailSize);
exportFile("../lite/cursortrail", liteTrailSize / 2, liteTrailSize / 2);

cursor.visible = true;
trail.hidden = true;

ring.fillColor = colorFromHex(cursorColor);
innerGlow.fillColor = colorFromHex(cursorColor);
glow.fillColor = colorFromHex(cursorColor);
glow.opacity = cursorGlowOpacity;
exportFile("../shared/cursor@2x", cursorSize, cursorSize);
exportFile("../shared/cursor", cursorSize / 2, cursorSize / 2);