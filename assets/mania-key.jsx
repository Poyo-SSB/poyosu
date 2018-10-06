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

var icon = document.pathItems.getByName("Icon");
var iconGlow = document.pathItems.getByName("Icon Glow");
var bigIcon= document.pathItems.getByName("Big Icon");
var bigIconGlow = document.pathItems.getByName("Big Icon Glow");

var keyBackground = document.pathItems.getByName("Key Background");
var barlineGlow = document.pathItems.getByName("Bar Line Glow");

var w = 89;
var h = 269;

function process(name, light, dark) {
    barlineGlow.fillColor = colorFromHex(light);
    keyBackground.fillColor = colorFromHex(light);
    
    icon.hidden = false;
    iconGlow.hidden = false;
    bigIcon.hidden = true;
    bigIconGlow.hidden = true;
    keyBackground.fillColor = colorFromHex(dark);
    iconGlow.fillColor = colorFromHex(light);
    exportFile("../shared/mania-key-" + name + "@2x", w, h);
    
    icon.hidden = true;
    iconGlow.hidden = true;
    bigIcon.hidden = false;
    bigIconGlow.hidden = false;
    keyBackground.fillColor = colorFromHex(light);
    bigIconGlow.fillColor = colorFromHex(light);
    exportFile("../shared/mania-keyD-" + name + "@2x", w, h);
}

process("red", "#bc0072", "#5e0039");
process("green", "#0ca800", "#065400");
process("blue", "#00607e", "#00303f");