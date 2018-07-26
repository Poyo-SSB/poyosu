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

var duck = document.rasterItems.getByName("Duck");
var quack = document.rasterItems.getByName("Quack");
var color = document.pathItems.getByName("Color");

duck.hidden = false;
quack.hidden = true;
color.fillColor = colorFromHex("#88d7fc");

exportFile("../shared/fruit-catcher-idle@2x", 622, 590);
exportFile("../shared/fruit-catcher-idle", 311, 295);

color.fillColor = colorFromHex("#ff6647");

exportFile("../shared/fruit-catcher-fail@2x", 622, 590);
exportFile("../shared/fruit-catcher-fail", 311, 295);

color.fillColor = colorFromHex("#55b5f4");

duck.hidden = true;
quack.hidden = false;
exportFile("../shared/fruit-catcher-kiai@2x", 622, 590);
exportFile("../shared/fruit-catcher-kiai", 311, 295);