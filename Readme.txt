
-------------------------------------------------------------
-------------------------------------------------------------
Mail:
 
Für das Beispiel gelten folgende Rahmenbedingungen:
 
Die Lösung ist in C# zu schreiben und der gesamte Sourcecode samt Projektfile und kompilierten Dateien abzugeben.
Die Lösung muss unter Windows 7/8/10 lauffähig sein.
Verwendet werden dürfen nur Funktionalitäten, die vom Microsoft .Net Framework (Version ist egal) zur Verfügung gestellt werden. Komponenten oder Codesnippets von Dritten sind nicht erlaubt.
Die Lösung ist von Ihnen selbst zu verfassen. Der Zeitaufwand sollte etwa 3-5 Stunden betragen, es geht uns nicht darum, eine bis ins Detail stabile oder außergewöhnliche Version zu erhalten, sondern darum, Ihren grundsätzlichen Programmierstil und grundlegendes technisches Hintergrundwissen zu evaluieren. Dementsprechend ist die zu lösende Aufgabe auch sehr einfach gehalten.
Die Lösung soll nicht "konstruiert" sein. Programmieren Sie so, wie Sie es immer tun bzw. im beruflichen Alltag tun würden.
 
Vielen Dank schon im Voraus.
 
(See attached file: Readme_V3.txt)
 
(See attached file: Sample.txt)
-------------------------------------------------------------
-------------------------------------------------------------
Readme:
Zeigen Sie uns mit Ihrer Implementierung dieses kleinen Beispiels, wie aus Ihrer Sicht qualitativ hochwertiger Code aussehen sollte! 

*** Szenario ***

Für einen Kunden wird ein kleines, einfaches Utility benötigt. Das Programm mit
Benutzeroberfläche soll eine angegebene Textdatei einlesen, die einzelnen
Wörter herausfiltern und für jedes Wort die Anzahl der Vorkommnisse zählen. Das
Ergebnis ist in Form einer einfachen Tabelle mit zwei Spalten auszugegeben.
Die erste Spalte enthält die gefundenen Wörter, die zweite Spalte die Anzahl der
Vorkommnisse. Eine Sortierung der Ergebnisse nach Anzahl Vorkommnisse ist gewünscht.

Die File-Parse Logik, die die Unterteilung in Wörter vornimmt, sollte 
modular entwickelt werden, um sie später für andere Projekte wieder verwenden zu 
können. Hohe Performance ist wünschenswert. Die Benutzeroberfläche sollte während 
der Bearbeitung "responsive" bleiben, damit der Kunde nicht das Gefühl bekommt, die
Anwendung würde nicht richtig funktionieren/nichts tun. 

*** Nähere Funktionsbeschreibung ***

Das Programm liest ANSI-Textdateien ein. Die Datei soll vom User angegeben
werden können. Die Trennung in Wörter erfolgt einzig aufgrund von Whitespaces (Space,
LF, CR, ...), auf die Behandlung von Satzzeichen muss nicht eingegangen werden.
Es werden vom Kunden größere Dateien (~50MB) verarbeitet, daher
sollte die Benutzeroberfläche einen Progress-Balken anzeigen. Eine Option zum
Abbruch ist ebenfalls erforderlich.

Siehe Datei Sample.txt für eine Beispieldatei.

Die folgende Datei...

1:1 Adam Seth Enos
1:2 Cainan Adam Seth Iared

sollte folgende Tabelle als Ergebnis ausgeben:

Wort		Anzahl
1:1		1
Adam		2
Seth		2
Enos		1
1:2		1
Cainan		1
Iared		1


Bitte beim Zusenden der Lösung das "bin" und "obj" Verzeichniss der Solution löschen, das sonst unsere
Firewall die Zustellung Ihres Mails unterbindet.
-----------------
Sample: 
1:1 Adam Seth Enos
1:2 Cainan Malelehel Iared
1:3 Enoch Matusale Lamech
1:4 Noe Sem Ham et Iafeth
1:5 filii Iafeth Gomer Magog Madai et Iavan Thubal Mosoch Thiras
1:6 porro filii Gomer Aschenez et Rifath et Thogorma
1:7 filii autem Iavan Elisa et Tharsis Cetthim et Dodanim
1:8 filii Ham Chus et Mesraim Phut et Chanaan
1:9 filii autem Chus Saba et Evila Sabatha et Rechma et
 Sabathaca porro filii Rechma Saba et Dadan
1:10 Chus autem genuit Nemrod iste coepit esse potens in terra
1:11 Mesraim vero genuit Ludim et Anamim et Laabim et Nepthuim
1:12 Phethrosim quoque et Chasluim de quibus egressi sunt
 Philisthim et Capthurim
1:13 Chanaan vero genuit Sidonem primogenitum et Heth
1:14 Iebuseum quoque et Amorreum et Gergeseum
1:15 Evheumque et Aruceum et Asineum
1:16 Aradium quoque et Samareum et Ematheum