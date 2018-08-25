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

var hexagon = document.pathItems.getByName("Hexagon");
var glow = document.compoundPathItems.getByName("Glow");

function process(hex, name, file) {
    document.groupItems.getByName("A").hidden = true;
    document.groupItems.getByName("B").hidden = true;
    document.groupItems.getByName("C").hidden = true;
    document.groupItems.getByName("D").hidden = true;
    document.groupItems.getByName("S").hidden = true;
    document.groupItems.getByName("SS").hidden = true;
    hexagon.fillColor = colorFromHex(hex);
    glow.pathItems[0].fillColor = colorFromHex(hex);
    document.groupItems.getByName(name).hidden = false;
    const big = 900;
    const small = 80;
    exportFile("../shared/ranking-" + file + "@2x", big, big);
    exportFile("../shared/ranking-" + file, big / 2, big / 2);
    exportFile("../shared/ranking-" + file + "-small@2x", small, small);
    exportFile("../shared/ranking-" + file + "-small", small / 2, small / 2);
}

process("#5fcd0b", "A", "A");
process("#026af3", "B", "B");
process("#bf17df", "C", "C");
process("#e20012", "D", "D");
process("#ff702e", "S", "S");
process("#8e8e8e", "S", "SH");
process("#ffbd0d", "SS", "X");
process("#bdbdbd", "SS", "XH");