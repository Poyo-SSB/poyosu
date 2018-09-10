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

function process(name, file) {
    document.layers.getByName("Osu").visible = false;
    document.layers.getByName("Taiko").visible = false;
    document.layers.getByName("Catch").visible = false;
    document.layers.getByName("Mania").visible = false;
    document.layers.getByName(name).visible = true;
    
    const med = 216;
    
    document.artboards[0].artboardRect = [-46, 46, 46, -46];
    exportFile("../shared/" + file + "-med@2x", med, med);
    
    document.artboards[0].artboardRect = [-46, 46 + 50, 46 + 6, -46];
    exportFile("../shared/" + file + "-small@2x", document.width, document.height);
}

process("Osu", "mode-osu");
process("Taiko", "mode-taiko");
process("Catch", "mode-fruits");
process("Mania", "mode-mania");