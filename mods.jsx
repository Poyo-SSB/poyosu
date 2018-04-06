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
    exportOptions.horizontalScale = (width / document.width) * 100;
    exportOptions.verticalScale = (height / document.height) * 100;
    
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
    document.rasterItems.getByName("Question Mark").hidden = true;
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
    exportFile("selection-mod-" + file + "@2x", size, size);
    exportFile("selection-mod-" + file, size / 2, size / 2);
}

process("#003f80", "Auto", "autoplay");
process("#3e3f3f", "Cinema", "cinema");
process("#592f86", "Double Time", "doubletime");
process("#4c741f", "Easy", "easy");
process("#7c4f01", "Question Mark", "fadein");
process("#7c4f01", "Question Mark", "fadeout");
process("#1e1e1e", "Flashlight", "flashlight");
process("#36323b", "Half Time", "halftime");
process("#7e0427", "Hard Rock", "hardrock");
process("#754d01", "Hidden", "hidden");
process("#201717", "1K", "key1");
process("#201717", "2K", "key2");
process("#201717", "3K", "key3");
process("#201717", "4K", "key4");
process("#201717", "5K", "key5");
process("#201717", "6K", "key6");
process("#201717", "7K", "key7");
process("#201717", "8K", "key8");
process("#201717", "9K", "key9");
process("#201717", "Question Mark", "keycoop");
process("#2d0087", "Nightcore", "nightcore");
process("#202755", "No Fail", "nofail");
process("#682f01", "Perfect", "perfect");
process("#005526", "Random", "random");
process("#01577f", "Relax", "relax");
process("#15305e", "Autopilot", "relax2");
process("#2e2e2e", "ScoreV2", "scorev2");
process("#3f0421", "Spun-out", "spunout");
process("#5d2a01", "Sudden Death", "suddendeath");
process("#42162c", "Target Practice", "target");