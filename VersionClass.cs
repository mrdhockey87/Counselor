using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    
    static class VersionClass
    {       
        static public string version_word = "Version:";
        static public string version_string = "4.3.7.54";

        static public string GetVersion()
        {
            return string.Format("{0} {1}", VersionClass.version_word, VersionClass.version_string);
        }

    }
}

/*
* 
* 
* 
 * v5.3.7.59 - More clean up of old code that Chris left behind mdail 7-19-2025
 * v5.3.7.58 - Clean up old code that was left behind from Chris, Trying to figure out where the database tables comes from  mdail 7-15-2025
 * v5.3.7.57 - Rehome t0 Github mdail 7-15-2025 
 * v5.3.7.56 - Fix to make it compile and run since windows obsoleted the old target framework and the old version of Dotfuscator was not working 
 *             with the new version of Visual Studio. mdail 7-14-2025
 * v4.3.7.55 - Fix crashing with null pointer exception when doing a search thru the documents(PDFs) list when doiing a soldier counseling.
 *             This also caused the search to return incorrect results as it would skip over node that should have been removed. mdail 4-11-2022
 * v4.3.7.54 - Finally found and set the extracts button tab to be not visible as requested mdail 4-7-2022
 * v4.3.7.53 - Tested for crashing on upload of custom documents and not being able to open the documents. can not verify send for further testing mdail 1-2-19 
 * v4.3.7.52 - Fix startup position of the splash screen to be the center of the main screens last saved location or the center of the screen mdail 1-2-20
 * v4.3.7.51 - After testing the new error reporting I set the error button on the main screen to not be visible as it is only there for testing mdail 10-23-19 
 * v4.3.7.50 - Added a button with text of error to the soldier dialog. This button is set to invisible. For testing of sending error report set the button to visible and
 *             click the button. It will send a crash file when clicked, it serves no other purpose. mdail 9-30-19
 * v4.3.7.49 - After converting to .Net framework 4.0 so I could use Microsoft.HttpClient client rewrote CloseLogAndGetErrorString as ErrorFIle which creates a JSon object 
 *             containing the error file which is called from SendCrashReport which is also rewritten and is what uses HttpClient to send the error report mdail 9-26-19
 * v4.2.6.48 - Trying to fix the error file transmitting to the web site I found the best way was going to be to convert the project to .net 4.0 this is right before the conversion mdail 9-26-19
 * v3.2.6.47 - Fixed the crash if you selected all or more than half of the suggested statement for a counseling because it was duplicating the list of suggestion because the
 *             form was loading twice. Also clean up some more code that Chris had left behind. (Matt's PDF page 1) mdail 8-28-19
 * v3.2.6.46 - Fixed the Dialog that asks if the user wants to submit a crash report getting set to short, now it check height and width and if below minimum it set them to
 *             minimum also made the min height and width and variable and did the same thing in the New Documents Preset Dialog as well. Also cleaned some more code out mdail 8-28-19
 * v3.2.6.45 - So far I am unable to duplicate the application stopping responding when trying to open a custom counseling upload PDF (Matt's PDF page 3) mdail 8-28-19
 * v3.2.6.44 - Set the date time picker on the upload my own document form to default to todays date when it first shows up (Matt's PDF page 2) mdail 8-28-19
 * v3.2.6.43 - Forced the application to try and send an error log to our website so Matt could see what it was sending to the site because it has had trouble sending logs
 *             for a long time. I pasted the log file that I forced it to try to send into an email and sent it to Matt mdail 8-27-19
 * v3.2.6.42 - Added a test to make sure that the document id returned to SelectedDocumentID is a valid int and if it's not return -1 to try and prevent crash if nothing is 
 *             selected in the data-grid, also added catch statements to the Convert Int32 method call that tries to convert the document id to an int for the same reason 
 *             (Matt's PDF page 4) this still might not fix the error, just possible mdail 8-27-19
 * v3.2.6.41 - Fixed the New Custom Report Dialog being sized too small, causing an overlap of the buttons and the last labels test. Check the size on load and save and resize if
 *             it's too small so it always fits all the control properly (Matt's PDF page 5) mdail 8-27-19
 * v3.2.6.40 - Fixed the link to Army Pub to the new correct link on the page and in the code, compressed the space that the links use on the page so they fit better on the smaller
 *             screen size so the last link is still visible (Matt's PDF page 6) mdail 8-27-19
 * v3.2.6.39 - Fixed Export Soldier Dialog duplicating the list of soldiers, it now check to see if the tree has any nodes and if it does it clears the tree before loading the 
 *             soldiers (Matt's PDF page 7) mdail 8-27-19
 * v3.2.6.38 - Fix the Options Dialog in the setting screen being set too small, no it check to make sure it is big enough on save and load and if it's not wide or high enough it fixes the 
 *             size to meet the minimum size required to display the screen (Matt's PDF page 8) mdail 8-27-19
 * v3.2.6.37 - Fix the Documents Tab (Counsellings Tab in Matt's PDF, page 9) not having the same color background as the rest of the APP mdail 8-27-19
 * v3.2.6.36 - Fix Label1 on the soldiers form cutting of the word Documents (page 10 of Matt's PDF counselor bugs.pdf, the last page) which says Remove the word and. He did remember it was supposed to have 
 *             the word Documents after the and. Removed old unused code from the Logger class. mdail 8-27-19
 * v3.2.5.35 - Had to make sure the putting the form to it's saved location was the first thing that was done when loading the forms, also removed so old code to help readability mdail 8-21-19
 * v3.2.5.34 - Fix LetterEditor Save and restore form location mdail 8-20-19
 * v3.2.5.33 - Added code to position and save the position the SoldierReportFiltersDialog, UploadUserGeneratedCounselingForm(Missed the code part yesterday) and XFDLExportOverflowWarningDialog,
 *             I also noticed that the load function of a lot of the form was disconnected so I am going through the form connecting them. Connected form and load: XFDLEditorPage2,
 *             XFDLEditorPage1, SelectTemplateValuesDialog, SelectSoldierDialog, SelectNewParentDocumentDialog, RecycleBinDialog, PrintHTMLForm, OptionsDialog, LetterEditor and AssessmentDialog
 *             all others where correct or didn't call a load and loaded form the class initialization.   forms mdail 8-20-19
 * v3.2.5.32 - Added code to position and save the position the NewDocumentPresetDialog, OptionsDialog, PromptToCompleteCounselingPackageDialog, PromptToSubmitCrashReport, RecycleBinDialog,
 *             SelectNewParentDocumentDialog, SelectSoldierDialog, and SelectTemplateValuesDialog forms mdail 8-19-19
 * v3.2.5.31 - Added code to position and save the position the AboutDialog, Form2, AssessmentDialog, ConfirmDetachDocumentDialog, ContinuationOfCounselingDialog, CounselingsForSoldier
 *             DocumentPropertiesDialog, DocumentsReportFilters, EditNoteDialog, EditSoldierDialog, ExportSoldiersDialog, LetterEditor, NewSoldierPage1Dialog, NewSoldierPage2Dialog,
 *             PDFViewerForm, PregnancyElectionStatementEditorStepOne, PregnancyElectionStatementStepTwo, PrintHTMLForm, SoldierInfoDialog, UploadUserGeneratedCounselingForm
 *             XFDLEditorPage1, XFDLEditorPage2, ImportConflictDialog and LegalNotice (Pick up after LegalNotice) forms mdail 8-16-19
 * v3.2.5.30 - Fixed it so the splash screen follows form1. where ever form1 is going to display the splash screen displays in at the top left corner of form1, if they are 
 *             going to display where they are not visible, then they are centered in the primary screen mdail 8-15-19
 * v3.2.5.29 - Got it to run after Dotfuscator runs on the assembly mdail 8-14-19
 * v3.2.4.28 - Trying to figure out how to run Dotfuscator without causing crashes on the application mdail 8-14-19
 * v3.2.4.27 - Added the database version number to the about screen just under the program version mdail 8-14-19
 * v2.1.4.26 - Changed to use the size instead of the minimum size of form1 to set the initial size of the form on load mdail 8-13-19
 * v2.1.3.25 - revered height and width mdail 8-13-19
 * v2.1.3.24 - Was resetting the location to a nil value after setting in code, fixed to set the proper location to about center mdail 8-13-19
 * v2.1.3.23 - Working on bug for screen position at start up mdail 8-13-19
 * v2.1.3.22 - Set it so if the windows form state = none (which is the default in the settings, then Form1 starts centered and it's normal size, else it starts the size and
 *             state that were saved the last time the APP was run mdail 8-13-19
 * v2.1.3.21 - Added the code to save the location of the main screen and restore it's position on start up mdail 8-13-19 
 * v2.1.3.20 - Added to code to start the splash screen at the center of the default monitor when the application starts. Also added the code to start the main (Form1) screen
 *             at the center of the main screen mdail 8-13-19
 * v2.1.3.19 - Replace the document and soldier button images on the recycle bin with the newest version of the images mdail 2-12-19
 * v2.1.3.18 - Fix Counseling tab back color, Fix next button down image on the XFDLEditorPage1, Fixed Soldier button on recycle bin not appearing disabled when it first loads
 *             Fix the edit button on the Counsellings and Documents section of the soldiers tab   mdail 2-12-19
 * v2.1.3.17 - Changed back to using just the SQL command in the save new document filter class. To fix bug if user adds name of report filter with single quote, added the replace which replaces a
               single quote with two single quotes so that it doesn't cause an error when inserting into the database. Also replace the tab images with new ones. mdail 2-14-19
 * v2.1.3.16 - Change the splash screen to the proper image and delete all the extra splash screen images, Added code to the AboutDialog to add the VersionClass.getVersion to the versionLabel
 *             when the screen is shown, also renamed this file and class as Version is a Key work in C# mdail 1-18-19
 * v2.1.3.15 - Changed the ACO banner so it will change when the image changes as I missed that one and fixed the background color of the splash screen mdail 1-15-19
 * v2.1.3.14 - Changed the background color of all the forms to the new color, change the way the background colors were being set from setting each forms
 *             background color to a RGB value to setting it to the settings default background color. Next time the background color needs to be
 *             changed all that needs to change is the default background color in the settings mdail 1-14-19
 * v2.1.2.13 - Updated with the newer images Matt gave me, Also updated the new buttons to have white text in code in the CQPGreenButton initial function 
 *             and added the GradientColor Class mdail 1-11-19
 * v2.1.2.12 - Change the Program.cs Main function to do the registration checking, Added a preprocessor co to determine whether in debug or not for validating key
 *             NOTE: Program is obfuscate with Dotfuscator that is an extension added to visual studio after the application is built in release mode  mdail 1-10-19
 * v2.1.2.11 - Update copy write date mdail 1-9-19
 * v2.0.2.10 - Fixed images on SelectNewParentDocumentDialog, SelectSoldierDialog, SelectTemplateValuesDialog, SoldierInfoDialog, SoldierReportFiltersDialog, SoldiersDialog,
 *             SplashScreen, UploadUserGeneratedCounselingForm, XFDLEditorPage1, XFDLExportOverflowWarningDialog, and XFDLEditorPage2 1-8-19 
 *  v2.0.2.9 - Starting again, fixed images on AboutDialog, AssessmentDialog, ContinuationOfCounselingDialog, CounselingsForSoldierForm, CounselingTab, 
 *             DocumentPropertiesDialog, DocumentsReportFiltersDialog, DocumentsTabPage, EditNoteDialog, EditSoldierDialog, ExportSoldiersDialog, 
 *             Form1, Form2, Form3, HelpTabPage.Designer, ImportConflictDialog, LegalNotice, NewDocumentPresetDialog, NewSoldierPage1Dialog, 
 *             NewSoldierPage2Dialog, OptionsDialog, PregnancyElectionStatementEditorStepOne, PregnancyElectionStatementStepTwo, 
 *             PromptToCompleteCounselingPackageDialog, PromptToSubmitCrashReport, ReassignChildDocumentsDialog, RecycleBinDialog,
 *             ReferencesTabPage and ReportsTab. Also had to manually fix the sizes of the Completed, Outstanding and Show All Report buttons mdail 1-7-19
 *  v2.0.1.8 - Set back to original files so I can start over and fix it correctly since I've figured out how to do it now. mdail 1-4-19
 *  v2.0.0.7 - SelectNewParentDocumentDialog, SelectSoldierDialog and SelectTemplateValuesDialog - Stopped to reset back and do over  mdail 1-4-19
 *  v2.0.0.6 - Fix embedded images on PregnancyElectionStatementEditorStepOne, {DocumentsTabPage, DocumentsReportFiltersDialog, AssessmentDialog, attachToDocumentButton
 *             DocumentPropertiesDialog, EditSoldierDialog, ExportSoldiersDialog, Form1, Form2, Form3, NewSoldierPage2Dialog, PromptToSubmitCrashReport, 
 *             and PromptToCompleteCounselingPackageDialog  (Fix correctly this time)}, reassignChildDocumentsButton, RecycleBinDialog, and ReportsTab mdail 1-3-19
 *  v2.0.0.6 - Fix embedded images on EditNoteDialog, ExportSoldiersDialog, HelpTabPage, ImportConflictDialog, LegalNotice, NewDocumentPresetDialog, NewSoldierPage1Dialog, 
 *             and OptionsDialog mdail 1-3-19
 *  v2.0.0.5 - Fix the embedded images on the DocumentsReportFiltersDialog, AboutDialog, ContinuationOfCounselingDialog, CounselingTab, and DocumentsTabPage mdail 1-3-19
 *  v2.0.0.4 - Fix the embedded graphics by converting them to linked graphics on the report tab and resized the report type
 *             buttons and anchored them to the top so the could be read properly note that after the APP was recompiled it converted 
 *             the images back to embedded mdail 1-2-19
 *  v2.0.0.3 - Fixed some of the graphics on the soldier dialog of form1  
 *  v2.0.0.2 - Replace the images with the new images the Matt made for the redesign of the application
 * 
 */
