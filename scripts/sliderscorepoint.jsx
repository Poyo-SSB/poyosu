﻿var document = app.activeDocument;

function exportFile(name, width, height) {
    var exportOptions = new ExportOptionsPNG24();
    exportOptions.matte = false;
    exportOptions.artBoardClipping = true;
    exportOptions.horizontalScale = 100 * width / document.width;
    exportOptions.verticalScale = 100 * height / document.height;
    
    var path = document.path + "/" + name + ".png"
    
    document.exportFile(new File(path), ExportType.PNG24, exportOptions);
}

exportFile("../shared/sliderscorepoint@2x", 50, 50);
exportFile("../shared/sliderscorepoint", 25, 25);