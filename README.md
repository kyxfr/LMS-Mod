# LMS Music (Last Man Standing Music)

A BepInEx mod for **Gorilla Tag** that plays cool, dramatic music when **you are the last person alive** in a round.

---

## What This Mod Does
- Plays music when you are the **last survivor**
- Music **fades in slowly** so it doesn’t startle you
- Music **fades out** when you get tagged or the round ends
- Music loops while you are still the last survivor

---

## What You Need
- **Gorilla Tag** (on Steam)
- **BepInEx 5.4.x** installed
- A PC that can already run Gorilla Tag mods

---

## How to Install (Easy Way)
1. Make sure BepInEx is already installed in your Gorilla Tag folder
2. Download `LMSMod.dll`
3. Put `LMSMod.dll` into:
   ```
   Gorilla Tag/BepInEx/plugins
   ```
4. Start Gorilla Tag

That’s it. No settings needed.

---

## How the Mod Knows When to Play Music
- The mod watches the game to see how many players are still alive
- If **everyone else is infected** and you are not:
  - The music fades in over 3 seconds
- If you get tagged or the round ends:
  - The music fades out smoothly

---

## Building the Mod Yourself (For People Who Want to Compile It)
Only do this if you want to build the mod on your own.

1. Open `LMSMod.csproj` in **Visual Studio**
2. Set the build mode to **Release**
3. Click **Build**
4. After building, find the file here:
   ```
   bin/Release/LMSMod.dll
   ```
5. Put that `.dll` file into:
   ```
   Gorilla Tag/BepInEx/plugins
   ```

---

## Notes
- The mod downloads the music automatically
- No buttons, no UI, it just works
- Designed to be lightweight and not hurt performance

Enjoy being the last one standing 🦍🎵

