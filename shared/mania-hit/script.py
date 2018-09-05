import bpy
import os


def set_percentage(percentage):
    bpy.data.scenes['Scene'].render.resolution_percentage = percentage


def set_rainbow(rainbow):
    bpy.data.materials['Glow'].node_tree.nodes["Mix"].inputs[0].default_value = 0 if rainbow else 1

def set_color(color):
    bpy.data.materials['Glow'].node_tree.nodes["RGB"].outputs[0].default_value = color


def set_text(text):
    bpy.data.objects['Text'].data.body = text
    bpy.data.objects['Text Glow'].data.body = text


filepath = bpy.data.filepath
directory = os.path.dirname(filepath)


def render_to(name):
    global directory

    base_path = os.path.join(directory, name)

    set_percentage(50)
    bpy.data.scenes['Scene'].render.filepath = os.path.join(
        base_path, 'mania-' + name + '@2x')
    bpy.ops.render.render(write_still=True)

    set_percentage(25)
    bpy.data.scenes['Scene'].render.filepath = os.path.join(
        base_path, 'mania-' + name)
    bpy.ops.render.render(write_still=True)


set_rainbow(True)

set_text('MAX')
render_to('hit300g')

set_rainbow(False)

set_text('300')
set_color((1, 0.8, 0, 1))
render_to('hit300')

set_text('200')
set_color((0.130137, 0.723055, 0.023153, 1))
render_to('hit200')

set_text('100')
set_color((0, 0.415686275, 1, 1))
render_to('hit100')

set_text('50')
set_color((0.25, 0.25, 0.25, 1))
render_to('hit50')

set_text('miss')
set_color((1, 0, 0, 1))
render_to('hit0')
