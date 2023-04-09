# Ratferences

Yet another Unity library that uses scriptable objects to tie references to their state. There are probably better ones out there, but I didn't want my scripts to depend on another library. Also, some of them were too big for my taste.

The name is combining the words Rat and References together. Not as big of a reach as some of the other ones!

## Overview

Scriptable object references are a way of decoupling game objects from each other while still letting them communicate. 

For instance, imagine you wanted to have access to your player's HP at any given time. Your player manager could have an `IntReference` that contains the current health value. It would be in charge of setting the value, but wouldn't be concerned about who is consuming it. Your HP UI display could request updates on that same `IntReference` and update the text whenever the value changes.

Neither of these implementations would depend on each other, and either component could be swapped out  without knowing about the other. All references derive from `ValueReference`, which lets you specify a `ValueChanged` callback that's executed on all value changes.

## Caveats

Scriptable objects maintain their value across the lifetime of your project and across scenes. Defining your defaults in a ValueReference is usually a bad idea since they can be overwritten.

### Adding New Value Types

New types can be added by subclassing `ValueReference`. For instance, here's the entire definition of `IntReference`.

```
using UnityEngine;

namespace Ratferences {
	[CreateAssetMenu(menuName = "KH/Reference/Int")]
	public class IntReference : ValueReference<int> { }
}
```

## Installation

There are two options for installation. One involves manually editing a file, and the other involves adding a URL to package manager.

NOTE: You should always back up your project before installing a new package.

### Add to Package Manager

Open the package manager (Window -> Package Manager), and hit the plus button (+) in the top right, then "add package from git URL". In that field, enter `https://github.com/khutchins/ratferences.git` and click Add.

### Modify manifest.json

Open Packages/manifest.json and add this to the list of dependencies (omitting the comma if it's at the end):

```
"com.khutchins.ratferences": "https://github.com/khutchins/ratferences.git",
```
