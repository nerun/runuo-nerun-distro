# RunUO is over!!!
[RunUO](https://github.com/runuo/runuo) is over. Official team abandoned it after version 2.3. Mark Sturgill, after take the reins for a while, officially abandoned it at version 2.6, with a few updates to 2.7 beta (released as 2.7 in Nov/2021 by Voxpire). So, and now? Mark Sturgill have recommended move to ServUO. I also recommend. **You will not need Nerun's Distro for ServUO**, it already have a Create World gump plus XMLSpawner by default.

* [ServUO on GitHub](https://github.com/ServUO/ServUO)

* [ServUO community](https://www.servuo.com/)

Just in case you still want to use RunUO to run shards of Ultima Online Classic Era, you can try this fork I made from RunUO. Basically I just updated to remove some annoying warnings and to be able to run it with Mono 5.2+ on Linux. (*I tested all versions of Mono 5 and the only one that compiles and runs RunUO 2.3/2.7 is 5.0.1.1. There no "odd" versions like 5.1*).

* [Nerun's RunUO fork on GitHub](https://github.com/Nerun/runuo)

If you want to try a RunUO experimental fork, with a strong .NET 5 foundation and excellent performance, you can try ModernUO. But don't use it to run a shard, it's still in development. But it deserves a look.

* [ModernUO on GitHub](https://github.com/modernuo/ModernUO)

---

# NERUN's DISTRO

## About
Nerun's Distro is an addon for RunUO 2.3 up to 2.7 beta centered on the settlement of the game, using the Premium Spawner engine. Easy to use, this addon includes spawns for a 100% spawned world: Felucca, Trammel, Ilshenar, Malas, Tokuno and Ter Mur. Tutorials in english and portuguese. Includes other scripts:

* CEO's Yet Another Arya Addon Generator v3.0
* Custom Regions in a Box 4.0
* Joeku's Automatic Speed Booster;
* Joeku's Staff Runebook;
* Joeku's Toolbar;
* Static Exporter;
* Talow's Stairs Addon v1.0;
* Termax's Staff Orb;
* Zen Archer's Spawn Editor v2.

## Content
There are two folders:

    ../Distro
    ../Patch for RunUO 2.3

## Installation
Take it easy here! You should choose the installation in accord with your RunUO version.

### Installing on RunUO 2.3
1. Copy folders inside "Distro" folder and paste them inside your RunUO folder (overwrite);

2. Copy folders inside "Patch for RunUO 2.3" and paste them inside your RunUO folder (overwrite);

You need both folders! But copy and paste folder "Distro" 1st, then "2.3" over "Distro"!

### Installing on RunUO 2.4+
1. Just copy folders inside "Distro" and paste them inside your RunUO folder (overwrite).

2. Ignore folder "Patch for RunUO 2.3".

## Usage
1. Use command _\[spawner_, it's the easiest beginning.
2. Then click _Apocalypse now_.
3. Now click _Let there be light_.
4. Now Select _Spawns by Expansion_. There are 3 options: Classic Spawns (pre-ML era), Mondain's Legacy and KR, SA and HS era.
5. Read tutorial inside 7-ZIP file for more details and learn how to become an "advanced user".

## Troubleshooting with Linux and Mono
ALWAYS use the latest Mono version, and ALWAYS download it from [Mono Official Download page](https://www.mono-project.com/download/stable/).

DON'T use Mono version in Debian repositories, it's OLD! Use anything above version 5.10!

Sometimes you can have problems when running RunUO after properly compile the server. If you receive any error message related to a "libz" missing, install these libraries:

    zlib1g
    zlib1g-dbg
    zlib1g-dev

In Debian Linux it's easy, just go to terminal and:

    sudo aptitude install zlib1g zlib1g-dbg zlib1g-dev
