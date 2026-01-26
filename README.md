# RunUO is over!!!

[RunUO](https://github.com/runuo/runuo) is effectively dead. The official team abandoned it after version 2.3. Mark Sturgill took over for a while, but eventually abandoned it as well at version 2.6, with a few updates leading to the 2.7 beta (released as 2.7 in Nov/2021 by Voxpire).

So, what now? Mark Sturgill recommended moving to ServUO — and I agree. **You will not need Nerun's Distro for ServUO**: it already includes a Create World gump and XMLSpawner by default.

* [ServUO on GitHub](https://github.com/ServUO/ServUO)
* [ServUO community](https://www.servuo.com/)

If you still want to use RunUO to run Classic Era Ultima Online shards, you can try this fork I made from RunUO. It is basically a cleanup fork: I removed some annoying warnings and made it run properly with Mono 5.2+ on Linux.
(*I tested all Mono 5.x versions, and the only one that reliably compiles and runs RunUO 2.3/2.7 is 5.0.1.1. There are no “odd” versions like 5.1.*)

* [WalkUO](https://github.com/nerun/WalkUO)

If you want to experiment with a RunUO-derived fork built on a modern .NET foundation with excellent performance, take a look at ModernUO. I wouldn’t recommend using it to run a production shard yet, since it’s still under heavy development, but it’s definitely worth checking out.

* [ModernUO on GitHub](https://github.com/modernuo/ModernUO)

---

# NERUN's DISTRO

## About

Nerun's Distro is an addon for RunUO versions 2.3 up to 2.7, focused on world settlement using the Premium Spawner engine. Easy to use, it includes spawns for a fully populated world: Felucca, Trammel, Ilshenar, Malas, Tokuno, and Ter Mur.

Tutorials are provided in both English and Portuguese. The distro also includes several additional scripts:

* CEO's Yet Another Arya Addon Generator v3.0
* Custom Regions in a Box 4.0
* Joeku's Automatic Speed Booster
* Joeku's Staff Runebook
* Joeku's Toolbar
* Static Exporter
* Talow's Stairs Addon v1.0
* Termax's Staff Orb
* Zen Archer's Spawn Editor v2

## Content

The package contains two folders:

```
../Distro
../Patch for RunUO 2.3
```

## Installation

> [!warning]
>
> This mod is distributed as an overlay and works by overwriting existing files.

Pay attention here: you must choose the correct installation method according to your RunUO version.

### Installing on RunUO 2.3 and 2.3.1

1. Copy the folders inside the "Distro" directory and paste them into your RunUO folder (overwrite existing files).

2. Copy the folders inside "Patch for RunUO 2.3" and paste them into your RunUO folder (overwrite existing files).

Both folders are required. **Always copy the "Distro" folder first, then apply the 2.3 patch over it.**

### Installing on RunUO 2.4 up to 2.7

1. Copy the folders inside "Distro" and paste them into your RunUO folder (overwrite existing files).

2. Ignore the "Patch for RunUO 2.3" folder.

## Usage

1. Use the command *[spawner* — this is the easiest way to start.
2. Click *Apocalypse now*.
3. Click *Let there be light*.
4. Select *Spawns by Expansion*. There are three options:
   * Classic Spawns (pre-ML era)
   * Mondain’s Legacy
   * KR, SA, and HS era
5. Read the tutorial included in the 7-ZIP file for more details and to learn how to use advanced features.

## Troubleshooting on Linux and Mono

RunUO will compile, but **will not run with Mono versions newer than 5.0.1.1**. See issues [#36](https://github.com/runuo/runuo/issues/36) and [#51](https://github.com/runuo/runuo/issues/51) for details.

In some cases, RunUO may fail to start even after a successful compilation. If you encounter errors related to a missing "libz", install the following packages:

```
zlib1g
zlib1g-dbg
zlib1g-dev
```

On Debian-based systems, run:

```
sudo apt install zlib1g zlib1g-dbg zlib1g-dev
```
