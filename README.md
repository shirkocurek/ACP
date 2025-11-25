# Create a README.md file again with the provided markdown content
readme_content = """# Academic College Planner (C#/.NET MAUI application)

Student-friendly academic tracker for **terms, courses, and assessments** with due-date setting, local notifications, and note-sharing—built with .NET MAUI and SQLite.

> **Why it exists:** The Academic College Planner* puts everything in one reliable mobile app with clean CRUD functionality, date pickers, and reminders that use the device OS.

---

## Table of Contents
- [Features](#features)
- [Screenshots](#screenshots)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Clone & Configure](#clone--configure)
  - [Run on iOS](#run-on-ios)
  - [Run on Android](#run-on-android)
  - [Seeding Demo Data](#seeding-demo-data)
- [Notifications](#notifications)
- [Building a Release & Publishing](#building-a-release--publishing)
- [Testing](#testing)
- [Project Structure](#project-structure)
- [Troubleshooting](#troubleshooting)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgements](#acknowledgements)

---

## Features

- **Terms, Courses, Assessments (CRUD)**
  - Add/edit/delete terms, courses, and two assessment types: *Objective* and *Performance*.
  - Validation: prevents null saves; ensures end dates are not before start dates.
  - Pickers: date pickers for all start/end fields; status picker (Planned/In Progress/Completed/Dropped).

- **Instructor Details & Notes**
  - Store instructor name, phone, and email (with email validation).
  - Add optional course notes and **share** via the system share sheet (email/SMS).

- **Local Notifications (Device OS)**
  - Opt-in alerts for **start** and **end** dates on courses and assessments.
  - Uses the phone’s native notification framework; no server required.

- **Usability Tweaks**
  - Cleaner list views (view first; edit/delete within details).
  - “Manage Assessments” entry point from the course details page.
  - Seeded demo data for quick evaluation.

---

## Screenshots

> Replace these with your own screenshots or remove the section.

- Android buttons clipping (fixed) — before/after  
  *(Attach images in GitHub and link them here.)*

---

## Tech Stack

- **Framework:** .NET 8, **.NET MAUI**
- **Language:** C# (MVVM via CommunityToolkit.MVVM)
- **Storage:** SQLite (`sqlite-net-pcl`)
- **Notifications:** `Plugin.LocalNotification`
- **Toolkit:** CommunityToolkit.Maui
- **Target Platforms:** Android, iOS, macOS (MacCatalyst)

---

## Architecture

- **MVVM** with ViewModels in `ViewModels/`, views/pages in `Pages/`, and UI helpers/controls in `Controls/`.
- **Data layer** via `Services/DatabaseService.cs` (async SQLite connection; schema migration helpers).
- **Notification layer** via `Services/NotificationService.cs` (permission prompt + schedule/cancel).
- **Dependency Injection** configured in `MauiProgram.cs`.  
- **Seeding** via `EnsureDemoDataAsync()` after `InitAsync()` to create one term, one course, and two assessments.

---

## Getting Started

### Prerequisites
- **.NET SDK 8** (project uses `global.json` to pin to 8.x)
- **MAUI Workloads** installed:
  ```bash
  dotnet workload install maui
  ```
- **Android**: Android SDK / emulator via Android Studio (or command line)
- **iOS (macOS)**: Xcode + iOS Simulator

> Verify SDK selection:
```bash
dotnet --version
dotnet --info
```
If multiple SDKs are installed, ensure `global.json` points to an 8.0 SDK (e.g., `8.0.100`) with `rollForward` set appropriately.

### Clone & Configure
```bash
git clone <your-repo-url>
cd c971-mobile-application-development-using-c-sharp
```

### Run on iOS
```bash
dotnet build -t:Run -f net8.0-ios
```

### Run on Android
Start an emulator (or connect a device), then:
```bash
dotnet build -t:Run -f net8.0-android
```

### Seeding Demo Data
On first run, the app creates tables and **seeds**:
- 1 term (“Fall Term …”),
- 1 course with instructor **Anika Patel** (555-123-4567, anika.patel@strimeuniversity.edu),
- Objective + Performance assessments.

If you previously installed the app and don’t see seed data, **uninstall** the app from the emulator/device and run again.

---

## Notifications

- The app requests permission once (per platform) the first time Terms page appears.
- You can enable/disable **start/end** notifications for each course/assessment.
- Notifications are scheduled locally; no server credentials or cloud scheduling.

> If you don’t see notifications, check device **App Notifications** settings and ensure the app is allowed to alert.

---

## Building a Release & Publishing

This project opts for a **simple, evaluator-friendly distribution**:  
- **GitHub Releases** host the signed APK.
- **GitHub Pages** hosts a lightweight landing page with download and usage instructions—no app store needed.

### Android: Build Signed APK
```bash
dotnet publish -f net8.0-android -c Release -p:AndroidPackageFormat=apk
# Output path is printed; rename the APK to: CollegeScheduler.apk
```
> If you need a custom keystore, add `-p:AndroidKeyStore=true ...` properties or configure in your csproj/Secrets.

### Create a GitHub Release
1. Tag the commit (e.g., `v1.0.1`).
2. Draft a Release in GitHub.
3. Upload **CollegeScheduler.apk**.
4. Include concise **release notes**: features, fixes, known issues, device requirements.

### Publish a Landing Page (GitHub Pages)
1. Enable Pages for branch `main` and folder `/docs`.
2. Add `/docs/index.html` linking to the **latest APK** on the Releases page, plus quick install steps and a short FAQ.
3. Commit & wait a minute for Pages to deploy.

---

## Testing

- **Unit/Component checks** (view models, validators, date rules).
- **Manual flows** (add/edit/delete across Terms/Courses/Assessments; share notes; notification prompt and scheduling).
- **Integration** (seed → create → edit → remove; verify date constraints and pickers).
- **Devices**: iOS Simulator, Android Emulator, and at least one physical Android device for notifications.

Deliverables:
- `docs/Test-Checklist.md` (scenarios + pass/fail)
- `CHANGELOG.md` (what changed in each release)

---

## Troubleshooting

- **Black screen on launch (iOS/Android):**
  - Ensure `MauiProgram.cs` builds the app **once** and seeds data: call `InitAsync()` then `EnsureDemoDataAsync()` after DI is built.
  - Verify DI setup for all ViewModels/Pages.

- **`ServiceHelper` not found:**
  - Confirm `Helpers/ServiceHelper.cs` exists and `ServiceHelper.Initialize(app.Services);` is called before page instantiation that uses it.

- **Android install error `ADB0010 / freeStorage(...) NRE`:**
  - Wipe/recreate emulator storage; ensure enough free space; try a fresh emulator.

- **No device available:**
  - Start an emulator or plug in a device, then run:
    ```bash
    dotnet build -t:Run -f net8.0-android
    ```

- **SDK mismatch (build uses .NET 9 by accident):**
  - Check `global.json` pins to **8.0.100** (or your approved 8.x) and run `dotnet --version` to confirm.

---

## Roadmap

- Export term/course calendars (ICS).
- Attachments for notes (links, files).
- Cloud sync / backup option.
- iOS TestFlight beta for broader UAT.

---

## Contributing

PRs welcome for bug fixes, small UX improvements, and documentation (spelling, screenshots).  
For larger features, please open an issue first to discuss scope and alignment with the educational rubric.

---

## License

Proprietary / Educational use (default).  
If you plan to open-source, add a proper OSI license (MIT/Apache-2.0).

---

## Acknowledgements

- .NET MAUI & CommunityToolkit teams  
- Plugin.LocalNotification maintainers  
- SQLite-net-pcl maintainers  
- Everyone who tested and provided feedback 🙌

---
