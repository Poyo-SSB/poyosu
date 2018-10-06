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

var body = document.pathItems.getByName("Body");
var note = document.pathItems.getByName("Note");

var w = 512;
var h = 102;

body.hidden = false;
note.hidden = true;

body.fillColor = colorFromHex("#BC0072");
exportFile("../shared/mania-noteL-red@2x", w, h);

body.fillColor = colorFromHex("#0CA800");
exportFile("../shared/mania-noteL-green@2x", w, h);

body.fillColor = colorFromHex("#00607E");
exportFile("../shared/mania-noteL-blue@2x", w, h);

body.hidden = true;
note.hidden = false;

exportFile("../shared/mania-note@2x", w, h);