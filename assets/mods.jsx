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

var background = document.pathItems.getByName("Background");

function process(hex, name, file) {
    document.rasterItems.getByName("Auto").hidden = true;
    document.rasterItems.getByName("Cinema").hidden = true;
    document.rasterItems.getByName("Double Time").hidden = true;
    document.rasterItems.getByName("Easy").hidden = true;
    document.rasterItems.getByName("Hidden").hidden = true;
    document.rasterItems.getByName("Flashlight").hidden = true;
    document.rasterItems.getByName("Half Time").hidden = true;
    document.rasterItems.getByName("Hard Rock").hidden = true;
    document.rasterItems.getByName("Nightcore").hidden = true;
    document.rasterItems.getByName("No Fail").hidden = true;
    document.rasterItems.getByName("Perfect").hidden = true;
    document.rasterItems.getByName("Random").hidden = true;
    document.rasterItems.getByName("Relax").hidden = true;
    document.rasterItems.getByName("Autopilot").hidden = true;
    document.rasterItems.getByName("ScoreV2").hidden = true;
    document.rasterItems.getByName("Spun-out").hidden = true;
    document.rasterItems.getByName("Sudden Death").hidden = true;
    document.rasterItems.getByName("Target Practice").hidden = true;
    document.rasterItems.getByName("Fade In").hidden = true;
    document.rasterItems.getByName("Fade Out").hidden = true;
    document.rasterItems.getByName("Co-op").hidden = true;
    document.rasterItems.getByName("1K").hidden = true;
    document.rasterItems.getByName("2K").hidden = true;
    document.rasterItems.getByName("3K").hidden = true;
    document.rasterItems.getByName("4K").hidden = true;
    document.rasterItems.getByName("5K").hidden = true;
    document.rasterItems.getByName("6K").hidden = true;
    document.rasterItems.getByName("7K").hidden = true;
    document.rasterItems.getByName("8K").hidden = true;
    document.rasterItems.getByName("9K").hidden = true;
    background.fillColor = colorFromHex(hex);
    document.rasterItems.getByName(name).hidden = false;
    
    const size = 132;
    exportFile("../shared/selection-mod-" + file + "@2x", size, size);
}

process("#0062ab", "Auto", "autoplay");
process("#525353", "Cinema", "cinema");
process("#8635c2", "Double Time", "doubletime");
process("#6eac19", "Easy", "easy");
process("#976700", "Fade In", "fadein");
process("#976700", "Fade Out", "fadeout");
process("#282828", "Flashlight", "flashlight");
process("#4c4355", "Half Time", "halftime");
process("#9d0034", "Hard Rock", "hardrock");
process("#a47700", "Hidden", "hidden");
process("#2f1e1e", "1K", "key1");
process("#2f1e1e", "2K", "key2");
process("#2f1e1e", "3K", "key3");
process("#2f1e1e", "4K", "key4");
process("#2f1e1e", "5K", "key5");
process("#2f1e1e", "6K", "key6");
process("#2f1e1e", "7K", "key7");
process("#2f1e1e", "8K", "key8");
process("#2f1e1e", "9K", "key9");
process("#2f1e1e", "Co-op", "keycoop");
process("#3f00a5", "Nightcore", "nightcore");
process("#202e80", "No Fail", "nofail");
process("#954a00", "Perfect", "perfect");
process("#007236", "Random", "random");
process("#00769e", "Relax", "relax");
process("#0b418e", "Autopilot", "relax2");
process("#3f3f3f", "ScoreV2", "scorev2");
process("#5c002e", "Spun-out", "spunout");
process("#954a00", "Sudden Death", "suddendeath");
process("#871953", "Target Practice", "target");