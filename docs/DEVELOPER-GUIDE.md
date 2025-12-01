User Guide for Initial Setup & Running the Application 

This guide is for maintainers of the Academic College Planner. It covers environment setup, routine operations, run, seed,and build as well as publishing updates to APK and GitHub Pages. 

1) Install and verify the following dependencies:
.NET 8 SDK

dotnet --version
     dotnet --info

.NET MAUI workloads

dotnet workload install maui
     dotnet workload list

Android: Install Android Studio, create an emulator, and enable USB debugging for devices.


iOS (macOS): Install Xcode and iOS Simulator.



2) Get the source code by running the following:
git clone https://github.com/shirkocurek/ACP


cd ACP
dotnet restore
dotnet build -c Debug

EXPECT: Build succeeded.


3) Run the App:
a. For Android run:
Start an emulator in Android Studio (AVD Manager) or plug in a device.

dotnet build -t:Run -f net8.0-android

b. For iOS (macOS) run:
dotnet build -t:Run -f net8.0-ios

EXPECT: App opens to the Terms view, and the seed data appears on first run.


4) Bump Version & Changelog:
Update app version (csproj/AssemblyInfo as appropriate).


Update CHANGELOG.md with features/fixes.



5) Build a signed Android APK:
dotnet publish -f net8.0-android -c Release -p:AndroidPackageFormat=apk

Locate the output path printed in the console.


Optional: Rename the artifact to AcademicCollegePlanner.apk.

6) Create a GitHub Release:
Open the releases page on GitHub and click ‘draft new release.’


Tag the commit with a version number v1.0.2.


Upload AcademicCollegePlanner.apk to the dependencies section on the release draft.


Add concise notes (features, fixes, or any known issues).

7) Publish/Update the landing page on GitHub Pages:
Go to Settings, then navigate to Pages and verify that Source = main and Folder = /docs.


Edit /docs/index.html to link to the latest Release asset.


Commit changes and let Pages deploy.


8) Verify Release:
Install the new APK on the emulator/device.


Run core flows: add/edit/delete Term/Course/Assessments. Validate dates and enable notifications.


Confirm that the Docs landing page download link works.


Confirm that the CHANGELOG is accurate.
9) Rollback (If Needed):
Point the landing page link back to a prior Release APK.


Note the rollback in the CHANGELOG.md file.


10) Minimal Test Pass coverage:
dotnet test tests/CollegeScheduler.Tests.csproj
EXPECT: All tests should pass and show green.

11) Quick Troubleshooting:
If you encounter the ‘No devices found’ error, run ‘adb devices’ to start an emulator in AVD Manager.


If you encounter a black screen/crash, run dotnet clean and dotnet build.


If you encounter a Pages 404 on APK
Ensure the APK is attached to the latest Release and the /docs/index.html URL matches.
