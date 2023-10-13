# PixelmonDatapackCreator
Create datapacks for Pixelmon mod with a graphical interface.

Appreciate the app? [Buy me a coffee :)](https://www.buymeacoffee.com/thingxii)


## Usage

### Launching
Upon launching the app, you will be greeted with a project selection screen. In this screen, enter the full path to the directory containing the following:
  - A folder called `species` that contains definitions of Pokemon that you want to load in.
  - A folder called `moves` that contains definitions of Moves that you want to load in.
  - A folder called `spawning` that contains definitions of Pokemon Spawns that you want to load in.
  - A folder called `assets` which contains an `editor` and `pokemon` folder.
    - `pokemon` contains sprite images for your datapack in the usual Pixelmon format.
    - `editor` contains `unknown.png` (for displaying unfindable sprites) and `defaultNewPokemon.json`, the default Pokemon when clicking Add Pokemon.
  - You can find a sample project packaged with each release.

### Modifying Pokemon
After loading a project, you will see Pokemon listed along the right side in a scroll bar. Selecting from this list will load the Pokemon and populate the
values in the stats and evolution windows.
  - 'Common' data is found on the left side. Above the splitter, this data is for the Pokemon itself. Below the black splitter and on the center panel, each value is for the selected *form* of the Pokemon.
    - This panel also allows you to select the form for your Pokemon. Often, the default form is simply empty - but alolan, mega, or gmax forms may exist with differing stats or evolutions as well!
  - The 'Stats' panel contains general form information for the Pokemon - like battle stats, aggression rates, etc. Changing the values here will change the final Pokemon when Save (in the top right) is clicked.
  - The 'Moves/Abilities' panel is currently unused.
  - The 'Evolutions' panel contains all evolution data for the selected form.
    - You can add an evolution by selecting 'Add Evolution' - then clicking on the unown sprite and populating the data on the left, in the Evolution Attribute panel.
    - After you have selected an evolution in the Evolution Preview window, you can add Evolution Conditions. These are 'and' conditions, meaning that each must be fulfilled for the evolution to take place in game.
      - Because of this, some Pokemon have multiple avenues to evolve into the same Pokemon - such as Eevee into Leafeon!
    - Evolutions also have an 'evo type'. Generally, this is `leveling` and is accompanied with the `level` attribute, but sometimes these can be different - such as Galarian Farfetch'd's `ticking` evolution that
occurs after it has hit 3 criticals in a single fight.


## Making changes
Currently, I am accepting Issues on the Issues tab above. You can suggest new features, report bugs, etc. there. If you wish to build on your own machine, I am using the following specs:
  - Unity 2021.3.19f1
