var document = app.activeDocument;

function exportFile(name, width, height) {
    var exportOptions = new ExportOptionsPNG24();
    exportOptions.matte = false;
    exportOptions.artBoardClipping = true;
    exportOptions.horizontalScale = (width / document.width) * 100;
    exportOptions.verticalScale = (height / document.height) * 100;
    
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
    exportFile(file + "-med@2x", med, med);
    exportFile(file + "-med", med / 2, med / 2);
    
    document.artboards[0].artboardRect = [-46, 46 + 50, 46 + 6, -46];
    exportFile(file + "-small@2x", document.width, document.height);
    exportFile(file + "-small", document.width / 2, document.height / 2);
}

process("Osu", "mode-osu");
process("Taiko", "mode-taiko");
process("Catch", "mode-fruits");
process("Mania", "mode-mania");