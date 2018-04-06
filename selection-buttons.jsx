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
    exportOptions.horizontalScale = (width / document.width) * 100;
    exportOptions.verticalScale = (height / document.height) * 100;
    
    var path = document.path + "/" + name + ".png"
    
    document.exportFile(new File(path), ExportType.PNG24, exportOptions);
}

var rectangle = document.pathItems.getByName("Rectangle");
var border = document.pathItems.getByName("Border");
var textItem = document.textFrames.getByName("Text");

function process(width, hex, text, file) {
    textItem.contents = text;
    
    document.artboards[0].artboardRect = [0, 0, width, -180];
    
    rectangle.fillColor = colorFromHex(hex);
    textItem.textRange.fillColor = colorFromHex("#ffffff");
    border.fillColor = colorFromHex("#ffffff");
    exportFile("selection-" + file + "@2x", width, 180);
    exportFile("selection-" + file, width / 2, 180 / 2);
    
    rectangle.fillColor = colorFromHex("#ffffff");
    textItem.textRange.fillColor = colorFromHex(hex);
    border.fillColor = colorFromHex(hex);
    exportFile("selection-" + file + "-over@2x", width, 180);
    exportFile("selection-" + file + "-over", width / 2, 180 / 2);
}

process(178, "#8b3bee", "", "mode");
process(148, "#d747ad", "Mods", "mods");
process(148, "#0096ed", "Options", "options");
process(148, "#8ed700", "Random", "random");