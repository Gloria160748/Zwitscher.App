﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zwitscher.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Impressum : ContentPage
    {
        public Impressum()
        {
            InitializeComponent();
            DatenschutzView.Text = DatenschutzText;
            ImpressumView.Text = ImpressumText;
        }

        public void ImpressumClicked(object sender, EventArgs e)
        {
            ImpressumView.IsVisible = !ImpressumView.IsVisible;
        }

        public void DatenschutzClicked(object sender, EventArgs e)
        {
            DatenschutzView.IsVisible = !DatenschutzView.IsVisible;
        }

        public void KontaktClicked(object sender, EventArgs e)
        {
            var emailMessage = "support@Zwitscher.de";
            Launcher.OpenAsync(new Uri("mailto:" + emailMessage));

        }

        public string DatenschutzText = "Datenschutzerklärung\r\n \r\nI. Name und Anschrift des Verantwortlichen\r\n \r\nDer Verantwortliche im Sinne der Datenschutz-Grundverordnung und anderer nationaler Datenschutzgesetze der Mitgliedsstaaten sowie sonstiger datenschutzrechtlicher Bestimmungen ist die:\r\nUniversität Siegen \r\nAdolf-Reichwein-Str. 2a \r\n57076 Siegen \r\nDeutschland \r\nTel.: 0271-740 - 0 \r\nE-Mail: info@uni-siegen.de \r\nWebsite: www.uni-siegen.de\r\n \r\nII. Kontaktdaten des Datenschutzbeauftragten\r\n \r\nDer Datenschutzbeauftragte des Verantwortlichen ist erreichbar unter:\r\nAdolf-Reichwein-Str. 2a \r\n57076 Siegen \r\nDeutschland \r\nTel.: +49 271 – 740 5147 \r\nE-Mail: datenschutzbeauftragter@uni-siegen.de\r\n \r\nIII. Allgemeines zur Datenverarbeitung\r\n \r\n1. Umfang der Verarbeitung personenbezogener Daten\r\nWir verarbeiten personenbezogene Daten unserer Nutzer grundsätzlich nur, soweit dies zur Bereitstellung einer funktionsfähigen Website sowie unserer Inhalte und Leistungen erforderlich ist. Die Verarbeitung personenbezogener Daten unserer Nutzer erfolgt regelmäßig nur nach Einwilligung des Nutzers. Eine Ausnahme gilt in solchen Fällen, in denen eine vorherige Einholung einer Einwilligung aus tatsächlichen Gründen nicht möglich ist und die Verarbeitung der Daten durch gesetzliche Vorschriften gestattet ist.\r\n \r\n1.1 Daten, die Sie uns zur Verfügung stellen.\r\nZur Nutzung einiger unserer Dienste benötigen Sie einen Account, und zur Erstellung eines Accounts müssen Sie uns bestimmte Daten zur Verfügung stellen. Grundsätzlich brauchen wir für viele unserer Dienste bestimmte Daten, damit Sie sie nutzen können.\r\n•\tPersönliche Accounts. Wenn Sie einen Account erstellen, müssen Sie uns einige Daten zur Verfügung stellen, damit wir Ihnen unsere Dienste bereitstellen können. Dazu gehören ein Name; ein Nutzername; ein Passwort; ein Geburtsdatum und ein Geschlecht. Ihre Profildaten, einschließlich Ihres Namens und Ihres Nutzernamens, sind immer öffentlich, aber Sie können entweder Ihren echten Namen oder ein Pseudonym verwenden. Denken Sie auch daran, dass Sie mehrere Zwitscher Accounts einrichten können, zum Beispiel wenn Sie verschiedene Aspekte Ihrer Identität zum Ausdruck bringen möchten, sei es beruflich oder anderweitig. \r\n1.2 Daten, die bei der Nutzung von Zwitscher erhoben werden. \r\nWenn Sie unsere Dienste nutzen, erheben wir Daten darüber, wie Sie unsere Produkte und Dienste nutzen. Mithilfe dieser Daten stellen wir Ihnen weitere Dienste zur Verfügung und tragen dazu bei, dass Zwitscher sicherer und respektvoller für alle und relevanter für Sie ist. \r\n \r\nDaten über Nutzung und Aktivitäten \r\n•\tBeiträge und andere Inhalte, die Sie posten.\r\n•\tIhre Interaktionen mit den Inhalten anderer Nutzer*innen, wie z. B. Rezwitscher, Like/Dislike-Markierungen und geteilte Inhalte.\r\n•\tWie Sie mit anderen auf der Plattform interagieren, beispielsweise mit Personen, denen Sie folgen, und Personen, die Ihnen folgen, einschließlich des Inhalts der Nachrichten, der Empfänger*innen sowie Datum und Uhrzeit der Nachrichten.\r\n•\tWenn Sie mit uns kommunizieren, erheben wir Daten über die Kommunikation und deren Inhalt.\r\n \r\nGerätedaten\r\n•\tDaten über Ihre Verbindung, wie beispielsweise Ihre IP-Adresse und den Browsertyp.\r\n•\tDaten über Ihr Gerät und seine Einstellungen, wie beispielsweise Geräte- und Werbe-ID, Betriebssystem, Anbieter, Sprache, Speicher, installierte Apps und Akkustand.\r\n•\tDas Adressbuch Ihres Geräts, wenn Sie sich dazu entschieden haben, es mit uns zu teilen.\r\n  \r\nProtokolldaten\r\nWir können Daten erhalten, wenn Sie sich Inhalte in unseren Produkten und Diensten ansehen oder anderweitig mit ihnen interagieren, auch wenn Sie keinen Account erstellt haben oder abgemeldet sind. Darunter fallen beispielsweise folgende Daten:\r\n•\tIP-Adresse; Browsertyp und -sprache; Betriebssystem; die verweisende Webseite; Zugriffszeiten; besuchte Seiten; Standort; Ihr Mobilfunkanbieter; Gerätedaten (einschließlich Geräte- und Applikations-IDs); Suchbegriffe und -IDs (einschließlich derer, die nicht als Abfragen übermittelt wurden); und Identifikatoren, die mit Cookies verknüpft sind. \r\n \r\nCookies und ähnliche Technologien\r\nWie viele andere Websites verwenden wir Cookies und ähnliche Technologien zur Erhebung zusätzlicher Daten über die Nutzung der Website und zum Betrieb unserer Dienste. Für viele Bereiche unserer Produkte und Dienste, wie beispielsweise die Suche und die Anzeige öffentlicher Profile, sind Cookies nicht erforderlich. Bei Cookies handelt es sich um Textdateien, die im Internetbrowser bzw. vom Internetbrowser auf dem Computersystem des Nutzers gespeichert werden. Ruft ein Nutzer eine Website auf, so kann ein Cookie auf dem Betriebssystem des Nutzers gespeichert werden. Dieser Cookie enthält eine charakteristische Zeichenfolge, die eine eindeutige Identifizierung des Browsers beim erneuten Aufrufen der Website ermöglicht.\r\nWir setzen Cookies ein, um unsere Website nutzerfreundlicher zu gestalten. Einige Elemente unserer Internetseite erfordern es, dass der aufrufende Browser auch nach einem Seitenwechsel identifiziert werden kann.\r\n \r\n2. Rechtsgrundlage für die Datenverarbeitung\r\nDie Rechtsgrundlage für die Verarbeitung personenbezogener Daten unter Verwendung von Cookies ist Art. 6 Abs. 1 lit. f DSGVO.\r\n \r\n3. Zweck der Datenverarbeitung\r\nDer Zweck der Verwendung technisch notwendiger Cookies ist, die Nutzung von Websites für die Nutzer zu vereinfachen. Einige Funktionen unserer Internetseite können ohne den Einsatz von Cookies nicht angeboten werden. Für diese ist es erforderlich, dass der Browser auch nach einem Seitenwechsel wiedererkannt wird.\r\nDie durch technisch notwendige Cookies erhobenen Nutzerdaten werden nicht zur Erstellung von Nutzerprofilen verwendet.\r\nIn diesen Zwecken liegt auch unser berechtigtes Interesse in der Verarbeitung der personenbezogenen Daten nach Art. 6 Abs. 1 lit. f DSGVO.\r\n \r\n4. Dauer der Speicherung, Widerspruchs- und Beseitigungsmöglichkeit\r\nCookies werden auf dem Rechner des Nutzers gespeichert und von diesem an unserer Seite übermittelt. Daher haben Sie als Nutzer auch die volle Kontrolle über die Verwendung von Cookies. Durch eine Änderung der Einstellungen in Ihrem Internetbrowser können Sie die Übertragung von Cookies deaktivieren oder einschränken. Bereits gespeicherte Cookies können jederzeit gelöscht werden. Dies kann auch automatisiert erfolgen. Werden Cookies für unsere Website deaktiviert, können möglicherweise nicht mehr alle Funktionen der Website vollumfänglich genutzt werden.\r\n \r\n5. Wie wir Daten verwenden\r\n\r\n5.1 Betreiben, Verbessern und Personalisieren unserer Dienste.\r\nDie von uns erhobenen Daten nutzen wir zur Bereitstellung und zum Betrieb der Zwitscher Produkte und Dienste. Anhand Ihrer Kontaktdaten helfen wir anderen, Ihren Account zu finden.\r\n5.2 Förderung von Schutz und Sicherheit.\r\nAnhand der von uns erhobenen Daten sorgen wir für den Schutz und die Sicherheit unserer Nutzer*innen, unserer Produkte, unserer Dienste und Ihres Accounts. Außerdem bewerten und beeinflussen wir mithilfe der Daten die Sicherheit und Qualität der Inhalte auf Zwitscher. Das beinhaltet die Untersuchung und Durchsetzung unserer Richtlinien und AGB sowie des geltenden Rechts.\r\n \r\n5.3 Messen, Analysieren und Verbessern unserer Dienst. \r\nAnhand der von uns erhobenen Daten messen und analysieren wir die Effektivität unserer Produkte und Dienste und lernen besser verstehen, wie Sie sie nutzen, damit wir sie weiter verbessern können.\r\n \r\n5.4 Kommunikation mit Ihnen über unsere Dienste. \r\nAnhand der von uns erhobenen Daten kommunizieren wir mit Ihnen über unsere Produkte und Dienste, u. a. über Änderungen unserer Richtlinien. \r\n \r\n5.5 Forschung.\r\nAnhand der Daten, die Sie uns mitteilen oder die wir erheben, führen wir Forschung, Umfragen, Produkttests und Fehlerbehebungen durch, die uns helfen, unsere Produkte und Dienste zu betreiben und zu verbessern.\r\n \r\n6. Weitergabe von Daten\r\nSie sollten wissen, auf welche Weise wir Ihre Daten weitergeben, warum wir sie weitergeben und wie Sie dies beeinflussen können. \r\n6.1 Wenn Sie posten und teilen.\r\nAn die breite Öffentlichkeit \r\nSie weisen uns an, diese Daten so weit wie möglich offenzulegen. Die Inhalte von Zwitscher, einschließlich Ihrer Profildaten (z. B. Name/Pseudonym, Nutzername, Profilbilder), sind für die breite Öffentlichkeit zur Ansicht zugänglich. Die Öffentlichkeit muss sich nicht anmelden, um einen Großteil der Inhalte auf Zwitscher zu sehen. Zwitscher Inhalte sind auch außerhalb von Zwitscher auffindbar, zum Beispiel über die Ergebnisse von Suchanfragen in Internet-Suchmaschinen.\r\nAn andere Zwitscher Nutzer*innen \r\nAbhängig von Ihren Einstellungen und basierend auf den Zwitscher Diensten, die Sie nutzen, geben wir Folgendes weiter:\r\n•\tIhre Interaktionen mit Zwitscher Inhalten anderer Nutzer*innen, wie beispielsweise Like/Dislike-Markierungen und Personen, denen Sie folgen.\r\n \r\n \r\n6.2 An Dritte und Integrationen von Dritten.\r\n Durch unsere APIs. Wir verwenden Technologien wie APIS und Einbettungen zur Bereitstellung öffentlicher Zwitscher Daten für Websites, Apps und andere zu ihrer Nutzung, zum Beispiel damit Posts auf einer Nachrichten-Website angezeigt werden können oder damit analysiert werden kann, was Personen auf Zwitscher äußern. \r\n \r\n7. Rechtsgrundlage für die Verarbeitung personenbezogener Daten\r\nSoweit wir für Verarbeitungsvorgänge personenbezogener Daten eine Einwilligung der betroffenen Person einholen, dient Art. 6 Abs. 1 lit. a EU-Datenschutzgrundverordnung (DSGVO) als Rechtsgrundlage.\r\nBei der Verarbeitung von personenbezogenen Daten, die zur Erfüllung eines Vertrages, dessen Vertragspartei die betroffene Person ist, erforderlich ist, dient Art. 6 Abs. 1 lit. b DSGVO als Rechtsgrundlage. Dies gilt auch für Verarbeitungsvorgänge, die zur Durchführung vorvertraglicher Maßnahmen erforderlich sind.\r\nSoweit eine Verarbeitung personenbezogener Daten zur Erfüllung einer rechtlichen Verpflichtung erforderlich ist, der unser Unternehmen unterliegt, dient Art. 6 Abs. 1 lit. c DSGVO als Rechtsgrundlage.\r\nFür den Fall, dass lebenswichtige Interessen der betroffenen Person oder einer anderen natürlichen Person eine Verarbeitung personenbezogener Daten erforderlich machen, dient Art. 6 Abs. 1 lit. d DSGVO als Rechtsgrundlage.\r\nIst die Verarbeitung zur Wahrung eines berechtigten Interesses unseres Unternehmens oder eines Dritten erforderlich und überwiegen die Interessen, Grundrechte und Grundfreiheiten des Betroffenen das erstgenannte Interesse nicht, so dient Art. 6 Abs. 1 lit. f DSGVO als Rechtsgrundlage für die Verarbeitung.\r\n \r\n8. Datenlöschung und Speicherdauer\r\nWir speichern verschiedene Arten von Daten für unterschiedliche Zeiträume:\r\n•\tWir speichern Ihre Profildaten und Inhalte für die Laufzeit Ihres Accounts.\r\n•\tAndere personenbezogene Daten, die wir bei der Nutzung unserer Produkte und Dienste erheben, speichern wir in der Regel für eine Dauer von maximal 18 Monaten.\r\n•\tDenken Sie daran, dass öffentliche Inhalte an anderer Stelle existieren können, auch nachdem Sie sie von Zwitscher entfernt haben. Zum Beispiel können Suchmaschinen und andere Dritte Kopien Ihrer Beiträge länger aufbewahren, basierend auf ihren eigenen Datenschutzerklärungen, auch nachdem sie auf Zwitscher gelöscht wurden oder abgelaufen sind. \r\n•\tWenn Sie gegen unsere Regeln verstoßen und Ihr Account gesperrt wird, können wir die Identifikatoren, die Sie zur Erstellung des Accounts verwendet haben, auf unbestimmte Zeit speichern. Dadurch wollen wir verhindern, dass Wiederholungstäter*innen neue Accounts erstellen.\r\n•\tZur Erfüllung gesetzlicher Anforderungen und aus Gründen der Sicherheit können wir mitunter bestimmte Daten länger aufbewahren, als in unseren Richtlinien angegeben ist.\r\n \r\n9. Übernehmen Sie die Kontrolle\r\n9.1 Zugriff, Berichtigung, Übertragbarkeit. \r\nSie können auf die Daten, die Sie uns zur Verfügung gestellt haben, zugreifen, sie berichtigen oder ändern, indem Sie Ihr Profil bearbeiten und Ihre Account-Einstellungen anpassen. \r\n9.2 Löschung Ihrer Daten. \r\nWenn Sie Ihren Zwitscher Account deaktivieren, sind der Account, Ihr Name, Ihr Nutzername und Ihr öffentliches Profil auf Zwitscher.de und in Zwitscher für Android nicht mehr sichtbar. \r\n \r\n9.3 Widerspruch, Einschränkung oder Rückzug Ihrer Einwilligung. \r\nÜber das Ticketsystem können Sie Ihre Datenschutzeinstellungen und andere Account-Funktionen verwalten. Wenn Sie Ihre Einstellungen ändern, kann es einige Zeit dauern, bis Ihre Auswahl in unseren Systemen vollständig umgesetzt ist. Abhängig davon, welche Einstellungen Sie angepasst haben, können Sie auch Änderungen bei Ihrer Zwitscher Nutzung oder Einschränkungen beim Zugriff auf bestimmte Funktionen feststellen. \r\nIV. Bereitstellung der Website und Erstellung von Logfiles\r\n \r\n1. Beschreibung und Umfang der Datenverarbeitung\r\nBei jedem Aufruf unserer Internetseite erfasst unser System automatisiert Daten und Informationen vom Computersystem des aufrufenden Rechners.\r\nFolgende Daten werden hierbei erhoben:\r\n2.\tInformationen über den Browsertyp und die verwendete Version\r\n3.\tDas Betriebssystem des Nutzers\r\n4.\tDen Internet-Service-Provider des Nutzers\r\n5.\tDie IP-Adresse des Nutzers\r\n6.\tDatum und Uhrzeit des Zugriffs\r\n7.\tWebsites, von denen das System des Nutzers auf unsere Internetseite gelangt\r\n8.\tWebsites, die vom System des Nutzers über unsere Website aufgerufen werden\r\nDie Daten werden ebenfalls in den Logfiles unseres Systems gespeichert. Eine Speicherung dieser Daten zusammen mit anderen personenbezogenen Daten des Nutzers findet nicht statt.\r\n\r\n2. Rechtsgrundlage für die Datenverarbeitung\r\nRechtsgrundlage für die vorübergehende Speicherung der Daten und der Logfiles ist Art. 6 Abs. 1 lit. f DSGVO.\r\n\r\n3. Zweck der Datenverarbeitung\r\nDie vorübergehende Speicherung der IP-Adresse durch das System ist notwendig, um eine Auslieferung der Website an den Rechner des Nutzers zu ermöglichen. Hierfür muss die IP-Adresse des Nutzers für die Dauer der Sitzung gespeichert bleiben.\r\nDie Speicherung in Logfiles erfolgt, um die Funktionsfähigkeit der Website sicherzustellen. Zudem dienen uns die Daten zur Optimierung der Website und zur Sicherstellung der Sicherheit unserer informationstechnischen Systeme. Eine Auswertung der Daten zu Marketingzwecken findet in diesem Zusammenhang nicht statt.\r\n \r\nIn diesen Zwecken liegt auch unser berechtigtes Interesse an der Datenverarbeitung nach Art. 6 Abs. 1 lit. f DSGVO.\r\n\r\n4. Dauer der Speicherung\r\nDie Daten werden gelöscht, sobald sie für die Erreichung des Zweckes ihrer Erhebung nicht mehr erforderlich sind. Im Falle der Erfassung der Daten zur Bereitstellung der Website ist dies der Fall, wenn die jeweilige Sitzung beendet ist.\r\nIm Falle der Speicherung der Daten in Logfiles ist dies nach spätestens 14 Tagen der Fall. Eine darüberhinausgehende Speicherung ist möglich. In diesem Fall werden die IP-Adressen der Nutzer gelöscht oder verfremdet, sodass eine Zuordnung des aufrufenden Clients nicht mehr möglich ist.\r\n \r\n5. Widerspruchs- und Beseitigungsmöglichkeit\r\nDie Erfassung der Daten zur Bereitstellung der Website und die Speicherung der Daten in Logfiles ist für den Betrieb der Internetseite zwingend erforderlich. Es besteht folglich seitens des Nutzers keine Widerspruchsmöglichkeit.\r\nV. Kontaktformular\r\n \r\n1. Beschreibung und Umfang der Datenverarbeitung\r\nAuf unserer Internetseite ist ein Kontaktformular vorhanden, welches für die elektronische Kontaktaufnahme genutzt werden kann. Nimmt ein Nutzer diese Möglichkeit wahr, so werden die in der Eingabemaske eingegeben Daten an uns übermittelt und gespeichert. Diese Daten sind:\r\n1.\tName\r\n2.\tVorname\r\n3.\tBenutzername\r\nIm Zeitpunkt der Absendung der Nachricht werden zudem folgende Daten gespeichert:\r\n1.\tDie IP-Adresse des Nutzers\r\n2.\tDatum und Uhrzeit der Absendung\r\n3.\tDie Adresse der Seite, von der aus die Kontaktaufnahme angestoßen wurde\r\n\r\n2. Rechtsgrundlage für die Datenverarbeitung\r\nRechtsgrundlage für die Verarbeitung der Daten ist bei Vorliegen einer Einwilligung des Nutzers Art. 6 Abs. 1 lit. a DSGVO.\r\n\r\n3. Zweck der Datenverarbeitung\r\nDie Verarbeitung der personenbezogenen Daten aus der Eingabemaske dient uns allein zur Bearbeitung der Kontaktaufnahme. Die während des Absendevorgangs verarbeiteten personenbezogenen Daten dienen dazu, einen Missbrauch des Kontaktformulars zu verhindern und die Sicherheit unserer informationstechnischen Systeme sicherzustellen.\r\n\r\n4. Dauer der Speicherung\r\nDie Daten werden gelöscht, sobald sie für die Erreichung des Zweckes ihrer Erhebung nicht mehr erforderlich sind. Für die personenbezogenen Daten aus der Eingabemaske des Kontaktformulars ist dies dann der Fall, wenn die jeweilige Konversation mit dem Nutzer beendet ist. Beendet ist die Konversation dann, wenn sich aus den Umständen entnehmen lässt, dass der betroffene Sachverhalt abschließend geklärt ist.\r\n\r\n5. Widerspruchs- und Beseitigungsmöglichkeit\r\nDer Nutzer hat jederzeit die Möglichkeit, seine Einwilligung zur Verarbeitung der personenbezogenen Daten zu widerrufen. \r\n \r\nVI. Die Zielgruppe von Zwitscher\r\nUnsere Dienste richten sich nicht an Kinder und Sie dürfen unsere Dienste nicht nutzen, wenn Sie unter 13 Jahre alt sind. \r\nVII. Änderungen an dieser Datenschutzerklärung\r\nDie aktuellste Version dieser Datenschutzerklärung regelt unsere Verarbeitung Ihrer personenbezogenen Daten und wir können diese Datenschutzerklärung gelegentlich nach Bedarf überarbeiten.\r\nWenn wir diese Datenschutzerklärung überarbeiten und Änderungen vornehmen, die wir als wesentlich erachten, werden wir Sie darüber informieren und Ihnen die Möglichkeit geben, die überarbeitete Datenschutzerklärung zu überprüfen, bevor Sie Zwitscher weiter nutzen.\r\n \r\nVIII. Rechte der betroffenen Person \r\nDie folgende Auflistung umfasst alle Rechte der Betroffenen nach der DSGVO. Rechte, die für die eigene Webseite keine Relevanz haben, müssen nicht genannt werden. Insoweit kann die Auflistung gekürzt werden. \r\nWerden personenbezogene Daten von Ihnen verarbeitet, sind Sie Betroffener i.S.d. DSGVO und es stehen Ihnen folgende Rechte gegenüber dem Verantwortlichen zu:\r\n\r\n1. Auskunftsrecht\r\nSie können von dem Verantwortlichen eine Bestätigung darüber verlangen, ob personenbezogene Daten, die Sie betreffen, von uns verarbeitet werden.\r\nLiegt eine solche Verarbeitung vor, können Sie von dem Verantwortlichen über folgende Informationen Auskunft verlangen:\r\n1.\tdie Zwecke, zu denen die personenbezogenen Daten verarbeitet werden;\r\n2.\tdie Kategorien von personenbezogenen Daten, welche verarbeitet werden;\r\n3.\tdie Empfänger bzw. die Kategorien von Empfängern, gegenüber denen die Sie betreffenden personenbezogenen Daten offengelegt wurden oder noch offengelegt werden;\r\n4.\tdie geplante Dauer der Speicherung der Sie betreffenden personenbezogenen Daten oder, falls konkrete Angaben hierzu nicht möglich sind, Kriterien für die Festlegung der Speicherdauer;\r\n5.\tdas Bestehen eines Rechts auf Berichtigung oder Löschung der Sie betreffenden personenbezogenen Daten, eines Rechts auf Einschränkung der Verarbeitung durch den Verantwortlichen oder eines Widerspruchsrechts gegen diese Verarbeitung;\r\n6.\tdas Bestehen eines Beschwerderechts bei einer Aufsichtsbehörde;\r\n7.\talle verfügbaren Informationen über die Herkunft der Daten, wenn die personenbezogenen Daten nicht bei der betroffenen Person erhoben werden;\r\n8.\tdas Bestehen einer automatisierten Entscheidungsfindung einschließlich Profiling gemäß Art. 22 Abs. 1 und 4 DSGVO und – zumindest in diesen Fällen – aussagekräftige Informationen über die involvierte Logik sowie die Tragweite und die angestrebten Auswirkungen einer derartigen Verarbeitung für die betroffene Person.\r\nIhnen steht das Recht zu, Auskunft darüber zu verlangen, ob die Sie betreffenden personenbezogenen Daten in ein Drittland oder an eine internationale Organisation übermittelt werden. In diesem Zusammenhang können Sie verlangen, über die geeigneten Garantien gem. Art. 46 DSGVO im Zusammenhang mit der Übermittlung unterrichtet zu werden.\r\n\r\n2. Recht auf Berichtigung\r\nSie haben ein Recht auf Berichtigung und/oder Vervollständigung gegenüber dem Verantwortlichen, sofern die verarbeiteten personenbezogenen Daten, die Sie betreffen, unrichtig oder unvollständig sind. Der Verantwortliche hat die Berichtigung unverzüglich vorzunehmen.\r\n\r\n3. Recht auf Einschränkung der Verarbeitung\r\nUnter den folgenden Voraussetzungen können Sie die Einschränkung der Verarbeitung der Sie betreffenden personenbezogenen Daten verlangen:\r\n1.\twenn Sie die Richtigkeit der Sie betreffenden personenbezogenen Daten für eine Dauer bestreiten, die es dem Verantwortlichen ermöglicht, die Richtigkeit der personenbezogenen Daten zu überprüfen;\r\n2.\tdie Verarbeitung unrechtmäßig ist und Sie die Löschung der personenbezogenen Daten ablehnen und stattdessen die Einschränkung der Nutzung der personenbezogenen Daten verlangen;\r\n3.\tder Verantwortliche die personenbezogenen Daten für die Zwecke der Verarbeitung nicht länger benötigt, Sie diese jedoch zur Geltendmachung, Ausübung oder Verteidigung von Rechtsansprüchen benötigen, oder\r\n4.\twenn Sie Widerspruch gegen die Verarbeitung gemäß Art. 21 Abs. 1 DSGVO eingelegt haben und noch nicht feststeht, ob die berechtigten Gründe des Verantwortlichen gegenüber Ihren Gründen überwiegen.\r\nWurde die Verarbeitung der Sie betreffenden personenbezogenen Daten eingeschränkt, dürfen diese Daten – von ihrer Speicherung abgesehen – nur mit Ihrer Einwilligung oder zur Geltendmachung, Ausübung oder Verteidigung von Rechtsansprüchen oder zum Schutz der Rechte einer anderen natürlichen oder juristischen Person oder aus Gründen eines wichtigen öffentlichen Interesses der Union oder eines Mitgliedstaats verarbeitet werden. \r\nWurde die Einschränkung der Verarbeitung nach den o.g. Voraussetzungen eingeschränkt, werden Sie von dem Verantwortlichen unterrichtet bevor die Einschränkung aufgehoben wird.\r\n\r\n4. Recht auf Löschung\r\na) Löschungspflicht\r\nSie können von dem Verantwortlichen verlangen, dass die Sie betreffenden personenbezogenen Daten unverzüglich gelöscht werden, und der Verantwortliche ist verpflichtet, diese Daten unverzüglich zu löschen, sofern einer der folgenden Gründe zutrifft:\r\n1.\tDie Sie betreffenden personenbezogenen Daten sind für die Zwecke, für die sie erhoben oder auf sonstige Weise verarbeitet wurden, nicht mehr notwendig.\r\n2.\tSie widerrufen Ihre Einwilligung, auf die sich die Verarbeitung gem. Art. 6 Abs. 1 lit. a oder Art. 9 Abs. 2 lit. a DSGVO stützte, und es fehlt an einer anderweitigen Rechtsgrundlage für die Verarbeitung.\r\n3.\tSie legen gem. Art. 21 Abs. 1 DSGVO Widerspruch gegen die Verarbeitung ein und es liegen keine vorrangigen berechtigten Gründe für die Verarbeitung vor, oder Sie legen gem. Art. 21 Abs. 2 DSGVO Widerspruch gegen die Verarbeitung ein.\r\n4.\tDie Sie betreffenden personenbezogenen Daten wurden unrechtmäßig verarbeitet.\r\n5.\tDie Löschung der Sie betreffenden personenbezogenen Daten ist zur Erfüllung einer rechtlichen Verpflichtung nach dem Unionsrecht oder dem Recht der Mitgliedstaaten erforderlich, dem der Verantwortliche unterliegt.\r\n6.\tDie Sie betreffenden personenbezogenen Daten wurden in Bezug auf angebotene Dienste der Informationsgesellschaft gemäß Art. 8 Abs. 1 DSGVO erhoben.\r\nb) Information an Dritte\r\nHat der Verantwortliche die Sie betreffenden personenbezogenen Daten öffentlich gemacht und ist er gem. Art. 17 Abs. 1 DSGVO zu deren Löschung verpflichtet, so trifft er unter Berücksichtigung der verfügbaren Technologie und der Implementierungskosten angemessene Maßnahmen, auch technischer Art, um für die Datenverarbeitung Verantwortliche, die die personenbezogenen Daten verarbeiten, darüber zu informieren, dass Sie als betroffene Person von ihnen die Löschung aller Links zu diesen personenbezogenen Daten oder von Kopien oder Replikationen dieser personenbezogenen Daten verlangt haben.\r\nc) Ausnahmen\r\nDas Recht auf Löschung besteht nicht, soweit die Verarbeitung erforderlich ist\r\n1.\tzur Ausübung des Rechts auf freie Meinungsäußerung und Information;\r\n2.\tzur Erfüllung einer rechtlichen Verpflichtung, die die Verarbeitung nach dem Recht der Union oder der Mitgliedstaaten, dem der Verantwortliche unterliegt, erfordert, oder zur Wahrnehmung einer Aufgabe, die im öffentlichen Interesse liegt oder in Ausübung öffentlicher Gewalt erfolgt, die dem Verantwortlichen übertragen wurde;\r\n3.\taus Gründen des öffentlichen Interesses im Bereich der öffentlichen Gesundheit gemäß Art. 9 Abs. 2 lit. h und i sowie Art. 9 Abs. 3 DSGVO;\r\n4.\tfür im öffentlichen Interesse liegende Archivzwecke, wissenschaftliche oder historische Forschungszwecke oder für statistische Zwecke gem. Art. 89 Abs. 1 DSGVO, soweit das unter Abschnitt a) genannte Recht voraussichtlich die Verwirklichung der Ziele dieser Verarbeitung unmöglich macht oder ernsthaft beeinträchtigt, oder\r\n5.\tzur Geltendmachung, Ausübung oder Verteidigung von Rechtsansprüchen.\r\n\r\n5. Recht auf Unterrichtung\r\nHaben Sie das Recht auf Berichtigung, Löschung oder Einschränkung der Verarbeitung gegenüber dem Verantwortlichen geltend gemacht, ist dieser verpflichtet, allen Empfängern, denen die Sie betreffenden personenbezogenen Daten offengelegt wurden, diese Berichtigung oder Löschung der Daten oder Einschränkung der Verarbeitung mitzuteilen, es sei denn, dies erweist sich als unmöglich oder ist mit einem unverhältnismäßigen Aufwand verbunden. \r\nIhnen steht gegenüber dem Verantwortlichen das Recht zu, über diese Empfänger unterrichtet zu werden.\r\n\r\n6. Recht auf Datenübertragbarkeit\r\nSie haben das Recht, die Sie betreffenden personenbezogenen Daten, die Sie dem Verantwortlichen bereitgestellt haben, in einem strukturierten, gängigen und maschinenlesbaren Format zu erhalten. Außerdem haben Sie das Recht diese Daten einem anderen Verantwortlichen ohne Behinderung durch den Verantwortlichen, dem die personenbezogenen Daten bereitgestellt wurden, zu übermitteln, sofern\r\n1.\tdie Verarbeitung auf einer Einwilligung gem. Art. 6 Abs. 1 lit. a DSGVO oder Art. 9 Abs. 2 lit. a DSGVO oder auf einem Vertrag gem. Art. 6 Abs. 1 lit. b DSGVO beruht und\r\n2.\tdie Verarbeitung mithilfe automatisierter Verfahren erfolgt.\r\nIn Ausübung dieses Rechts haben Sie ferner das Recht, zu erwirken, dass die Sie betreffenden personenbezogenen Daten direkt von einem Verantwortlichen einem anderen Verantwortlichen übermittelt werden, soweit dies technisch machbar ist. Freiheiten und Rechte anderer Personen dürfen hierdurch nicht beeinträchtigt werden. \r\nDas Recht auf Datenübertragbarkeit gilt nicht für eine Verarbeitung personenbezogener Daten, die für die Wahrnehmung einer Aufgabe erforderlich ist, die im öffentlichen Interesse liegt oder in Ausübung öffentlicher Gewalt erfolgt, die dem Verantwortlichen übertragen wurde.\r\n\r\n7. Widerspruchsrecht\r\nSie haben das Recht, aus Gründen, die sich aus ihrer besonderen Situation ergeben, jederzeit gegen die Verarbeitung der Sie betreffenden personenbezogenen Daten, die aufgrund von Art. 6 Abs. 1 lit. e oder f DSGVO erfolgt, Widerspruch einzulegen; dies gilt auch für ein auf diese Bestimmungen gestütztes Profiling. \r\nDer Verantwortliche verarbeitet die Sie betreffenden personenbezogenen Daten nicht mehr, es sei denn, er kann zwingende schutzwürdige Gründe für die Verarbeitung nachweisen, die Ihre Interessen, Rechte und Freiheiten überwiegen, oder die Verarbeitung dient der Geltendmachung, Ausübung oder Verteidigung von Rechtsansprüchen. \r\nWerden die Sie betreffenden personenbezogenen Daten verarbeitet, um Direktwerbung zu betreiben, haben Sie das Recht, jederzeit Widerspruch gegen die Verarbeitung der Sie betreffenden personenbezogenen Daten zum Zwecke derartiger Werbung einzulegen; dies gilt auch für das Profiling, soweit es mit solcher Direktwerbung in Verbindung steht. \r\nWidersprechen Sie der Verarbeitung für Zwecke der Direktwerbung, so werden die Sie betreffenden personenbezogenen Daten nicht mehr für diese Zwecke verarbeitet. \r\nSie haben die Möglichkeit, im Zusammenhang mit der Nutzung von Diensten der Informationsgesellschaft – ungeachtet der Richtlinie 2002/58/EG – Ihr Widerspruchsrecht mittels automatisierter Verfahren auszuüben, bei denen technische Spezifikationen verwendet werden.\r\n\r\n8. Recht auf Widerruf der datenschutzrechtlichen Einwilligungserklärung\r\nSie haben das Recht, Ihre datenschutzrechtliche Einwilligungserklärung jederzeit zu widerrufen. Durch den Widerruf der Einwilligung wird die Rechtmäßigkeit der aufgrund der Einwilligung bis zum Widerruf erfolgten Verarbeitung nicht berührt.\r\n\r\n9. Automatisierte Entscheidung im Einzelfall einschließlich Profiling\r\nSie haben das Recht, nicht einer ausschließlich auf einer automatisierten Verarbeitung – einschließlich Profiling – beruhenden Entscheidung unterworfen zu werden, die Ihnen gegenüber rechtliche Wirkung entfaltet oder Sie in ähnlicher Weise erheblich beeinträchtigt. Dies gilt nicht, wenn die Entscheidung\r\n1.\tfür den Abschluss oder die Erfüllung eines Vertrags zwischen Ihnen und dem Verantwortlichen erforderlich ist,\r\n2.\taufgrund von Rechtsvorschriften der Union oder der Mitgliedstaaten, denen der Verantwortliche unterliegt, zulässig ist und diese Rechtsvorschriften angemessene Maßnahmen zur Wahrung Ihrer Rechte und Freiheiten sowie Ihrer berechtigten Interessen enthalten oder\r\n3.\tmit Ihrer ausdrücklichen Einwilligung erfolgt.\r\nAllerdings dürfen diese Entscheidungen nicht auf besonderen Kategorien personenbezogener Daten nach Art. 9 Abs. 1 DSGVO beruhen, sofern nicht Art. 9 Abs. 2 lit. a oder g DSGVO gilt und angemessene Maßnahmen zum Schutz der Rechte und Freiheiten sowie Ihrer berechtigten Interessen getroffen wurden. \r\nHinsichtlich der in (1) und (3) genannten Fälle trifft der Verantwortliche angemessene Maßnahmen, um die Rechte und Freiheiten sowie Ihre berechtigten Interessen zu wahren, wozu mindestens das Recht auf Erwirkung des Eingreifens einer Person seitens des Verantwortlichen, auf Darlegung des eigenen Standpunkts und auf Anfechtung der Entscheidung gehört.\r\n\r\n10. Recht auf Beschwerde bei einer Aufsichtsbehörde\r\nUnbeschadet eines anderweitigen verwaltungsrechtlichen oder gerichtlichen Rechtsbehelfs steht Ihnen das Recht auf Beschwerde bei einer Aufsichtsbehörde, insbesondere in dem Mitgliedstaat ihres Aufenthaltsorts, ihres Arbeitsplatzes oder des Orts des mutmaßlichen Verstoßes, zu, wenn Sie der Ansicht sind, dass die Verarbeitung der Sie betreffenden personenbezogenen Daten gegen die DSGVO verstößt. \r\nDie Aufsichtsbehörde, bei der die Beschwerde eingereicht wurde, unterrichtet den Beschwerdeführer über den Stand und die Ergebnisse der Beschwerde einschließlich der Möglichkeit eines gerichtlichen Rechtsbehelfs nach Art. 78 DSGVO.\r\n";
        public string ImpressumText = "Impressum\r\nUniversität Siegen \r\nAdolf-Reichwein-Straße 2a \r\n57076 Siegen \r\nE-Mail: presse@uni-siegen.de \r\nTelefon: +49 271 740-0 \r\nTelefax: +49 271 740-4911 \r\n\r\nDie Universität Siegen ist eine vom Land Nordrhein-Westfalen getragene, rechtsfähige Körperschaft des öffentlichen Rechts. \r\nSie wird vertreten durch den Rektor Univ.-Prof. Dr. Holger Burckhart. \r\n\r\nZuständige Aufsichtsbehörde\r\nMinisterium für Kultur und Wissenschaft des Landes Nordrhein-Westfalen. Umsatzsteuer-Identifikationsnummer gemäß § 27 a Umsatzsteuergesetz: DE 154854171. \r\nRedaktionell verantwortlich\r\ngemäß § 5 des Telemediengesetzes und § 55 Absatz 2 des Rundfunkstaatsvertrages: Autoren, die am Ende der jeweiligen Internet-Seiten genannt sind. \r\nDaneben gibt es diverse dezentrale Angebote, z. T. auf eigenen Servern, für die jeweils eigene Verantwortliche ersichtlich sind. \r\nVerantwortlich für den Internet-Auftritt der Universität Siegen (ohne Fakultäten und Einrichtungen): Presse- und Kommunikationsstelle der Universität Siegen (Anschrift wie oben).\r\n\r\nGestaltung:\r\nAufbau und Gestaltung der Webseiten unterliegen - wie andere visuelle Kommunikationsmedien - der Universität Siegen einheitlichen Gestaltungsvorgaben, die in einem \"Corporate Design Manual\" beschrieben sind, und deren Umsetzung durch Beispiele, Vorlagen und weitere Hilfsmittel unterstützt wird.\r\n\r\nTechnische Durchführung:\r\nZentrum für Informations- und Medientechnologie (ZIMT)\r\n\r\nHaftungsausschluss\r\nTrotz sorgfältiger inhaltlicher Kontrolle übernehmen wir keine Haftung für die Inhalte externer Links. Sie stellen kein Angebot der Universität Siegen dar. Für den Inhalt der verlinkten Seiten sind ausschließlich deren Betreiber verantwortlich. Die Universität distanziert sich ausdrücklich von den Inhalten und macht sich diese in keinster Weise zu Eigen.\r\n\r\nCopyright\r\nDie Urheber- und Nutzungsrechte (Copyright) für Texte, Grafiken, Bilder, Design und Quellcode liegen bei der Universität Siegen. Die Erstellung, Verwendung und Weitergabe von Kopien in elektronischer oder ausgedruckter Form bedarf der Genehmigung.\r\n\r\n\r\n";
    }
}