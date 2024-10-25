# SabreTools.Skippers

This library comprises of code to perform copier header operations such as matching, manipulation, and removal. These are used for many older console-based systems and helps define known header skippers for immediate use.

Find the link to the Nuget package [here](https://www.nuget.org/packages/SabreTools.Skippers).

## Headerer

A small tool that allows users to extract, store, and remove copier headers for a variety of systems. Optionally, the stored headers can be replaced on the original files using a separate command. Each of the headers are stored in a Sqlite database file that links each header to the unheadered hash of the original file. The following copier header types are supported:

* Atari 7800
* Atari Lynx
* Commodore 64 PSID music
* NEC PC-Engine / TurboGrafx 16
* Nintendo Famicom Disk System
* Nintendo Entertainment System / Famicom
* Super Nintendo Entertainment System / Super Famicom
* Super Nintendo Entertainment System / Super Famicom SPC music
