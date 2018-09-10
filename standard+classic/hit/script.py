import bpy
import os


def set_scale(scale):
    bpy.data.cameras['Camera'].ortho_scale = scale


def set_percentage(percentage):
    bpy.data.scenes['Scene'].render.resolution_percentage = percentage


def set_color(color):
    bpy.data.materials['Glow'].node_tree.nodes['Emission'].inputs['Color'].default_value = color


def set_text(text):
    bpy.data.objects['Text'].data.body = text
    bpy.data.objects['Text Glow'].data.body = text


def set_font(font_name):
    bpy.data.curves['Text'].font = bpy.data.fonts[font_name]
    bpy.data.curves['Text Glow'].font = bpy.data.fonts[font_name]


def set_size(size):
    bpy.data.curves['Text'].size = size
    bpy.data.curves['Text Glow'].size = size


def hidden(thing, should):
    bpy.data.objects[thing].hide = should
    bpy.data.objects[thing].hide_render = should


filepath = bpy.data.filepath
directory = os.path.dirname(filepath)


def render_to(name, multiplier=1):
    global directory

    base_path = os.path.join(directory, name)

    set_scale(8.5)
    set_percentage(160)

    for i in range(bpy.data.scenes['Scene'].frame_start, bpy.data.scenes['Scene'].frame_end):
        bpy.data.scenes['Scene'].frame_set(i)
        bpy.data.scenes['Scene'].render.filepath = os.path.join(
            base_path, name + '-' + str(i) + '@2x')
        bpy.ops.render.render(write_still=True)

    bpy.data.scenes['Scene'].frame_set(0)
    set_scale(multiplier)

    set_percentage(50)
    bpy.data.scenes['Scene'].render.filepath = os.path.join(
        base_path, name + '@2x')
    bpy.ops.render.render(write_still=True)


hidden('White Ring', True)
hidden('Rainbow', True)
hidden('Text', False)
hidden('Text Glow', False)
set_font('Exo')
set_size(0.78)

set_text('300')
set_color((0.057805, 0.462077, 0.693872, 1))
render_to('hit300', 1.3)

set_text('100')
set_color((0.130137, 0.723055, 0.023153, 1))
render_to('hit100', 1.3)

set_text('50')
set_color((0.637597, 0.401978, 0.078187, 1))
render_to('hit50', 1.3)

set_color((1, 0.017, 0., 1))
hidden('Text', True)
hidden('Text Glow', True)
hidden('Miss', False)
hidden('Miss Glow', False)
render_to('hit0')

hidden('Miss', True)
hidden('Miss Glow', True)
hidden('Text', False)
hidden('Text Glow', False)
hidden('White Ring', False)
hidden('Rainbow', False)
set_font('Noto')
set_size(1.56)

set_text('激')
set_color((0.057805, 0.462077, 0.693872, 1))
render_to('hit300g')

set_text('喝')
set_color((0.057805, 0.462077, 0.693872, 1))
render_to('hit300k')

set_text('喝')
set_color((0.130137, 0.723055, 0.023153, 1))
render_to('hit100k')
