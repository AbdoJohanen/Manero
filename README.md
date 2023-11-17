# Manero
Grupp 1

Vi har arbetat med lokala databaser, så ni behöver skapa en databas för Products och en för Identity.

INSTRUKTIONER FÖR TEST:

GeorgeTests: 
För att mina tester skulle kunna köras, vänligen ange sökvägen till "wwwroot"-mappen på era individuella datorer och infoga den i GeorgeTests/GetAllIntegrationTests/ProductServiceIntegrationTests/TestWebHostEnvironment/WebRootPath.

LucasTester: I ShopControllerTest måste webRootPath ändras till den korrekta pathen som är annorlunda på varje dator. 
Exempelvis: "C:\\Users\\lukep\\Desktop\\Manero - Projekt\\Manero\\Manero\\wwwroot\\"

Malin&Mira:
"ConnectionStringTests" så måste ni lägga till sin egna lokala databas connection string.
"GetProductImagesTest", "GetProductNameTest" och "ProductsControllerTests" måste ni lägga till er egen sökväg till wwwroot.

---------------------------------------------------------------------------------------------------------------------------------

MANIFEST

Gruppmedlemmar: 
Dennis Gustavsson, Elvin Javadov, Simon Bäcklin, Lucas Nykvist, George Bdewi, Ludvig Franck, Abdo Johanen, Mira Wänseth, Malin Eriksson 


Dagliga möten
Daily Standup 10.00 varje vardag.
Måndagar reflekterar vi över grupparbetet.


Frånvaro/VAB/sjukdom
Är man sjuk, vabbar så behöver man inte ta igen. Viktigt att kommunicera frånvaro.
Kommunicera via Teams, kan man inte närvara vid daily standup kan man istället skriva vad man gjort.


Delaktigheten under lektionsdagarna och självstudiedagarna
Vi förväntar oss att alla kollar på föreläsningar samma dag som de släpps.
Alla som kan närvarar vid handledningstillfällena för att ta del av genomgångar.
Frihet under ansvar.


Sprintredovisning - Redovisning och avstämning för gruppen av det man gjort
Alla är med och aktivt planerar redovisningen och deltar i mötet. 


Arbetstider
Under planeringen första veckan av arbetet (user stories osv) sitter vi två och två/i helgrupp. Därefter får alla sitta när de vill. 


Arbetsverktyg och andra rutiner.
Vi har valt att använda Jira. 
Möten sker i Teams. 
Vi kommer använda GitHub. 


Gemensamt arbete

Allt som rör databas gör vi gemensamt i grupp, såsom entities, databaskopplingar och databasstrukturer.

Parprogrammering

Alla vill prova på att parprogrammera. Vi kommer att parprogrammera utifrån egna önskemål, man kontaktar någon i vår chatt t.ex.


Jira
Vi arbetar i en gemensam scrumboard i Jira. Vi har en backlog med projektets alla user stories. Vi planerar gemensamt vilka user stories som ska in i varje sprint. User stories som är pågående men som väntar på att en annan del ska bli klar lägger vi i on hold. 


Tekniska lösningar

Design för responsivt
Vi designar mobile first och utgår från iPhone SE-storleken (375px w)
Utgår från Bootstrap och siktar på S, M och XL breakpoints.

Designmöte om stor skärm design hålls vid senare tillfälle.


Kodstruktur

Vi gör nya sections för varje del på sidan, exempelvis Featured Section osv. Klassen på sections döps namnet på delen + section i kebab-case, exempelvis featured-products-section.


All css-kod använder vi kebab-case. 
Alla klasser använder vi kebab-case och försöker namnge så logiskt och lättförståeligt som möjligt. Vi använder inga förkortningar, förutom btn. 


I övrig kod skriver vi camelCase / PascalCase 



Frontend 
ASP.NET C#
MVC
Sass
Bootstrap
Fontawesome
Google Fonts
JS


Backend
.NET
Entity Framework Core
Lokal databas


Test
Vi testar vår kod mot lokal databas.

Github/Git
Vi har en develop branch, i den har vi branches för varje feature. När man skapar user stories sparar man på tillhörande feature. När man är klar med feature så mergar man in i develop branchen. Vid mergekonfliker hjälps vi åt, speciellt vid större konflikter.
Vid fel hjälps vi åt i gruppen.
Kommandon: 

Innan man skapar en ny branch, gör en pull på develop-branchen. (git pull origin develop)
git branch namnetpåfeaturen (skapar en branch)
git checkout namnetpåfeaturen (gör att man hamnar i feature branchen)

SPARA:
git add .
git commit -m “valfri text”
git push origin namnetpåfeaturen

Man sparar sitt arbete löpande under dagen.
