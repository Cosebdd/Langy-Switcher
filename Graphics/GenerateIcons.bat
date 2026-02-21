magick -background none icon.svg -define icon:auto-resize=24 icon24.ico
magick -background none icon.svg -define icon:auto-resize=16 icon16.ico
magick -background none icon.svg -define icon:auto-resize=16,24,32,48,64,128,256 Logo.ico

move /Y "*.ico" "..\Langy.UI\"