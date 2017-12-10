# Introduction to WPF panels

Panels are one of the most important control types of WPF. They act as containers for other controls and control the layout of your windows/pages. Since a window can only contain ONE child control, a panel is often used to divide up the space into areas, where each area can contain a control or another panel (which is also a control, of course).

+++

Panels come in several different flavors, with each of them having its own way of dealing with layout and child controls. Picking the right panel is therefore essential to getting the behavior and layout you want, and especially in the start of your WPF career, this can be a difficult job. The next section will describe each of the panels shortly and give you an idea of when to use it. After that, move on to the next chapters, where each of the panels will be described in detail.

+++

## Canvas

A simple panel, which mimics the WinForms way of doing things. It allows you to assign specific coordinates to each of the child controls, giving you total control of the layout. This is not very flexible though, because you have to manually move the child controls around and make sure that they align the way you want them to. Use it (only) when you want complete control of the child control positions.

+++

## WrapPanel

The WrapPanel will position each of its child controls next to the other, horizontally (default) or vertically, until there is no more room, where it will wrap to the next line and then continue. Use it when you want a vertical or horizontal list controls that automatically wraps when there's no more room.

+++

## StackPanel

The StackPanel acts much like the WrapPanel, but instead of wrapping if the child controls take up too much room, it simply expands itself, if possible. Just like with the WrapPanel, the orientation can be either horizontal or vertical, but instead of adjusting the width or height of the child controls based on the largest item, each item is stretched to take up the full width or height. Use the StackPanel when you want a list of controls that takes up all the available room, without wrapping.

+++

## DockPanel

The DockPanel allows you to dock the child controls to the top, bottom, left or right. By default, the last control, if not given a specific dock position, will fill the remaining space. You can achieve the same with the Grid panel, but for the simpler situations, the DockPanel will be easier to use. Use the DockPanel whenever you need to dock one or several controls to one of the sides, like for dividing up the window into specific areas.

+++

## Grid

The Grid is probably the most complex of the panel types. A Grid can contain multiple rows and columns. You define a height for each of the rows and a width for each of the columns, in either an absolute amount of pixels, in a percentage of the available space or as auto, where the row or column will automatically adjust its size depending on the content. Use the Grid when the other panels doesn't do the job, e.g. when you need multiple columns and often in combination with the other panels.

+++

## UniformGrid

The UniformGrid is just like the Grid, with the possibility of multiple rows and columns, but with one important difference: All rows and columns will have the same size! Use this when you need the Grid behavior without the need to specify different sizes for the rows and columns.

---

# The Canvas control

The Canvas is probably the simplest Panel of them all. It doesn't really do anything by default, it just allows you to put controls in it and then position them yourself using explicit coordinates.

+++

If you have ever used another UI library like WinForms, this will probably make you feel right at home, but while it can be tempting to have absolute control of all the child controls, this also means that the Panel won't do anything for you once the user starts resizing your window, if you localize absolutely positioned text or if the content is scaled.

+++

This one is mostly about showing you just how little the Canvas does by default:

```XML
<Window x:Class="WpfTutorialSamples.Panels.Canvas"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Canvas" Height="200" Width="200">
        <Canvas>
                <Button>Button 1</Button>
                <Button>Button 2</Button>
        </Canvas>
</Window>
```

+++

![A simple Canvas](http://www.wpf-tutorial.com/chapters/panels/images/canvas_simple.png "A simple Canvas")

+++

As you can see, even though we have two buttons, they are both placed in the exact same place, so only the last one is visible. The Canvas does absolutely nothing until you start giving coordinates to the child controls. This is done using the Left, Right, Top and Bottom attached properties from the Canvas control.

+++

These properties allow you to specify the position relative to the four edges of the Canvas. By default, they are all set to NaN (Not a Number), which will make the Canvas place them in the upper left corner, but as mentioned, you can easily change this:

```XML
<Window x:Class="WpfTutorialSamples.Panels.Canvas"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Canvas" Height="200" Width="200">
        <Canvas>
                <Button Canvas.Left="10">Top left</Button>
                <Button Canvas.Right="10">Top right</Button>
                <Button Canvas.Left="10" Canvas.Bottom="10">Bottom left</Button>
                <Button Canvas.Right="10" Canvas.Bottom="10">Bottom right</Button>
        </Canvas>
</Window>
```

+++

![A simple Canvas, where we position the child elements](http://www.wpf-tutorial.com/chapters/panels/images/canvas_with_positions.png "A simple Canvas, where we position the child elements")

Notice how I only set the property or properties that I need. For the first two buttons, I only wish to specify a value for the X axis, so I use the Left and Right properties to push the buttons towards the center, from each direction.

+++

For the bottom buttons, I use both Left/Right and Bottom to push them towards the center in both directions. You will usually specify either a Top or a Bottom value and/or a Left or a Right value.

+++

As mentioned, since the Canvas gives you complete control of positions, it won't really care whether or not there's enough room for all your controls or if one is on top of another. This makes it a bad choice for pretty much any kind of dialog design, but the Canvas is, as the name implies, great for at least one thing: Painting. WPF has a bunch of controls that you can place inside a Canvas, to make nice illustrations.

+++

## Z-Index

In the next example, we'll use a couple of the shape related controls of WPF to illustrate another very important concept when using the Canvas: Z-Index. Normally, if two controls within a Canvas overlaps, the one defined last in the markup will take precedence and overlap the other(s). However, by using the attached ZIndex property on the Panel class, this can easily be changed.

+++

First, an example where we don't use z-index at all:

```XML
<Window x:Class="WpfTutorialSamples.Panels.CanvasZIndex"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="CanvasZIndex" Height="275" Width="260">
    <Canvas>
        <Ellipse Fill="Gainsboro" Canvas.Left="25" Canvas.Top="25" Width="200" Height="200" />
        <Rectangle Fill="LightBlue" Canvas.Left="25" Canvas.Top="25" Width="50" Height="50" />
        <Rectangle Fill="LightCoral" Canvas.Left="50" Canvas.Top="50" Width="50" Height="50" />
        <Rectangle Fill="LightCyan" Canvas.Left="75" Canvas.Top="75" Width="50" Height="50" />
    </Canvas>
</Window>
```

+++

![A Canvas with overlapping elements, not using the ZIndex property](http://www.wpf-tutorial.com/chapters/panels/images/canvas_no_zindex.png "A Canvas with overlapping elements, not using the ZIndex property")

+++

Notice that because each of the rectangles are defined after the circle, they all overlap the circle, and each of them will overlap the previously defined one. Let's try changing that:

```XML
<Window x:Class="WpfTutorialSamples.Panels.CanvasZIndex"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="CanvasZIndex" Height="275" Width="260">
    <Canvas>
        <Ellipse Panel.ZIndex="2" Fill="Gainsboro" Canvas.Left="25" Canvas.Top="25" Width="200" Height="200" />
        <Rectangle Panel.ZIndex="3" Fill="LightBlue" Canvas.Left="25" Canvas.Top="25" Width="50" Height="50" />
        <Rectangle Panel.ZIndex="2" Fill="LightCoral" Canvas.Left="50" Canvas.Top="50" Width="50" Height="50" />
        <Rectangle Panel.ZIndex="4" Fill="LightCyan" Canvas.Left="75" Canvas.Top="75" Width="50" Height="50" />
    </Canvas>
</Window>
```

+++

![A Canvas with overlapping elements, using the ZIndex property](http://www.wpf-tutorial.com/chapters/panels/images/canvas_zindex.png "A Canvas with overlapping elements, using the ZIndex property")

+++

The default ZIndex value is 0, but we assign a new one to each of the shapes. The rule is that the element with the higher z-index overlaps the ones with the lower values. If two values are identical, the last defined element "wins". As you can see from the screenshot, changing the ZIndex property gives quite another look.

---

# The WrapPanel control

The **WrapPanel** will position each of its child controls next to the other, horizontally (default) or vertically, until there is no more room, where it will wrap to the next line and then continue. Use it when you want a vertical or horizontal list controls that automatically wraps when there's no more room.

+++

When the WrapPanel uses the Horizontal orientation, the child controls will be given the same height, based on the tallest item. When the WrapPanel is the Vertical orientation, the child controls will be given the same width, based on the widest item.

+++

In the first example, we'll check out a WrapPanel with the default (Horizontal) orientation:

```XML
<Window x:Class="WpfTutorialSamples.Panels.WrapPanel"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="WrapPanel" Height="300" Width="300">
        <WrapPanel>
                <Button>Test button 1</Button>
                <Button>Test button 2</Button>
                <Button>Test button 3</Button>
                <Button Height="40">Test button 4</Button>
                <Button>Test button 5</Button>
                <Button>Test button 6</Button>
        </WrapPanel>
</Window>
```

+++

![WrapPanel in Horizontal mode](http://www.wpf-tutorial.com/chapters/panels/images/wrappanel_horizontal.png "WrapPanel in Horizontal mode")

+++

Notice how I set a specific height on one of the buttons in the second row. In the resulting screenshot, you will see that this causes the entire row of buttons to have the same height instead of the height required, as seen on the first row. You will also notice that the panel does exactly what the name implies: It wraps the content when it can't fit any more of it in. In this case, the fourth button couldn't fit in on the first line, so it automatically wraps to the next line.

+++

Should you make the window, and thereby the available space, smaller, you will see how the panel immediately adjusts to it:

![WrapPanel in Horizontal mode](http://www.wpf-tutorial.com/chapters/panels/images/wrappanel_horizontal_smaller.png "WrapPanel in Horizontal mode")

+++

All of this behavior is also true when you set the Orientation to Vertical. Here's the exact same example as before, but with a Vertical WrapPanel:

```XML
<Window x:Class="WpfTutorialSamples.Panels.WrapPanel"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="WrapPanel" Height="120" Width="300">
        <WrapPanel Orientation="Vertical">
                <Button>Test button 1</Button>
                <Button>Test button 2</Button>
                <Button>Test button 3</Button>
                <Button Width="140">Test button 4</Button>
                <Button>Test button 5</Button>
                <Button>Test button 6</Button>
        </WrapPanel>
</Window>
```

+++

![WrapPanel in Vertical mode](http://www.wpf-tutorial.com/chapters/panels/images/wrappanel_vertical.png "WrapPanel in Vertical mode")

+++

You can see how the buttons go vertical instead of horizontal, before they wrap because they reach the bottom of the window. In this case, I gave a wider width to the fourth button, and you will see that the buttons in the same column also gets the same width, just like we saw with the button height in the Horizontal example.

+++

Please be aware that while the Horizontal WrapPanel will match the height in the same row and the Vertical WrapPanel will match the width in the same column, height is not matched in a Vertical WrapPanel and width is not matched in a Horizontal WrapPanel. Take a look in this example, which is the Vertical WrapPanel but where the fourth button gets a custom width AND height:

```XML
<Button Width="140" Height="44">Test button 4</Button>
```

+++

It will look like this:

![WrapPanel in Vertical mode with specific width/heights](http://www.wpf-tutorial.com/chapters/panels/images/wrappanel_vertical_width_and_height.png "WrapPanel in Vertical mode with specific width/heights")

Notice how button 5 only uses the width - it doesn't care about the height, although it causes the sixth button to be pushed to a new column.

---

# The StackPanel control

The **StackPanel** is very similar to the WrapPanel, but with at least one important difference: The StackPanel doesn't wrap the content. Instead it stretches it content in one direction, allowing you to stack item after item on top of each other. Let's first try a very simple example, much like we did with the WrapPanel:

+++

```XML
<Window x:Class="WpfTutorialSamples.Panels.StackPanel"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="StackPanel" Height="160" Width="300">
        <StackPanel>
                <Button>Button 1</Button>
                <Button>Button 2</Button>
                <Button>Button 3</Button>
                <Button>Button 4</Button>
                <Button>Button 5</Button>
                <Button>Button 6</Button>
        </StackPanel>
</Window>
```

+++

![A simple StackPanel in Vertical mode](http://www.wpf-tutorial.com/chapters/panels/images/stackpanel_vertical_simple.png "A simple StackPanel in Vertical mode")

+++

The first thing you should notice is how the StackPanel doesn't really care whether or not there's enough room for the content. It doesn't wrap the content in any way and it doesn't automatically provide you with the ability to scroll (you can use a ScrollViewer control for that though - more on that in a later chapter).

+++

You might also notice that the default orientation of the StackPanel is Vertical, unlike the WrapPanel where the default orientation is Horizontal. But just like for the WrapPanel, this can easily be changed, using the Orientation property:

```XML
<StackPanel Orientation="Horizontal">
```

+++

![A simple StackPanel in Horizontal mode](http://www.wpf-tutorial.com/chapters/panels/images/stackpanel_horizontal_simple.png "A simple StackPanel in Horizontal mode")

+++

Another thing you will likely notice is that the StackPanel stretches its child control by default. On a vertically aligned StackPanel, like the one in the first example, all child controls get stretched horizontally. On a horizontally aligned StackPanel, all child controls get stretched vertically, as seen above. The StackPanel does this by setting the HorizontalAlignment or VerticalAlignment property on its child controls to Stretch, but you can easily override this if you want to. 

+++

Have a look at the next example, where we use the same markup as we did in the previous example, but this time we assign values to the VerticalAlignment property for all the child controls:

```XML
<Window x:Class="WpfTutorialSamples.Panels.StackPanel"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="StackPanel" Height="160" Width="300">
        <StackPanel Orientation="Horizontal">
                <Button VerticalAlignment="Top">Button 1</Button>
                <Button VerticalAlignment="Center">Button 2</Button>
                <Button VerticalAlignment="Bottom">Button 3</Button>
                <Button VerticalAlignment="Bottom">Button 4</Button>
                <Button VerticalAlignment="Center">Button 5</Button>
                <Button VerticalAlignment="Top">Button 6</Button>
        </StackPanel>
</Window>
```

+++

![A StackPanel in Vertical mode with differently aligned controls](http://www.wpf-tutorial.com/chapters/panels/images/stackpanel_children_vertical_alignment.png "A StackPanel in Vertical mode with differently aligned controls")

+++

We use the Top, Center and Bottom values to place the buttons in a nice pattern, just for kicks. The same can of course be done for a vertically aligned StackPanel, where you would use the HorizontalAlignment on the child controls:

```XML
<Window x:Class="WpfTutorialSamples.Panels.StackPanel"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="StackPanel" Height="160" Width="300">
        <StackPanel Orientation="Vertical">
                <Button HorizontalAlignment="Left">Button 1</Button>
                <Button HorizontalAlignment="Center">Button 2</Button>
                <Button HorizontalAlignment="Right">Button 3</Button>
                <Button HorizontalAlignment="Right">Button 4</Button>
                <Button HorizontalAlignment="Center">Button 5</Button>
                <Button HorizontalAlignment="Left">Button 6</Button>
        </StackPanel>
</Window>
```

+++

![A StackPanel in Horizontal mode with differently aligned controls](http://www.wpf-tutorial.com/chapters/panels/images/stackpanel_children_horizontal_alignment.png "A StackPanel in Horizontal mode with differently aligned controls")

As you can see, the controls still go from top to bottom, but instead of having the same width, each control is aligned to the left, the right or center.

---

# The DockPanel control

The **DockPanel** makes it easy to dock content in all four directions (top, bottom, left and right). This makes it a great choice in many situations, where you want to divide the window into specific areas, especially because by default, the last element inside the DockPanel, unless this feature is specifically disabled, will automatically fill the rest of the space (center).

+++

As we've seen with many of the other panels in WPF, you start taking advantage of the panel possibilities by using an attached property of it, in this case the DockPanel.Dock property, which decides in which direction you want the child control to dock to. If you don't use this, the first control(s) will be docked to the left, with the last one taking up the remaining space. Here's an example on how you use it:

+++

```XML
<Window x:Class="WpfTutorialSamples.Panels.DockPanel"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DockPanel" Height="250" Width="250">
        <DockPanel>
                <Button DockPanel.Dock="Left">Left</Button>
                <Button DockPanel.Dock="Top">Top</Button>
                <Button DockPanel.Dock="Right">Right</Button>
                <Button DockPanel.Dock="Bottom">Bottom</Button>
                <Button>Center</Button>
        </DockPanel>
</Window>
```

+++

![A simple DockPanel](http://www.wpf-tutorial.com/chapters/panels/images/dockpanel_simple.png "A simple DockPanel")

+++

As already mentioned, we don't assign a dock position for the last child, because it automatically centers the control, allowing it to fill the remaining space. You will also notice that the controls around the center only takes up the amount of space that they need - everything else is left for the center position. That is also why you will see the Right button take up a bit more space than the Left button - the extra character in the text simply requires more pixels.

+++

The last thing that you will likely notice, is how the space is divided. For instance, the Top button doesn't get all of the top space, because the Left button takes a part of it. The DockPanel decides which control to favor by looking at their position in the markup. In this case, the Left button gets precedence because it's placed first in the markup. Fortunately, this also means that it's very easy to change, as we'll see in the next example, where we have also evened out the space a bit by assigning widths/heights to the child controls:

+++

```XML
<Window x:Class="WpfTutorialSamples.Panels.DockPanel"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DockPanel" Height="250" Width="250">
        <DockPanel>
                <Button DockPanel.Dock="Top" Height="50">Top</Button>
                <Button DockPanel.Dock="Bottom" Height="50">Bottom</Button>
                <Button DockPanel.Dock="Left" Width="50">Left</Button>
                <Button DockPanel.Dock="Right" Width="50">Right</Button>        
                <Button>Center</Button>
        </DockPanel>
</Window>
```

+++

![A DockPanel where width or heights has been specified for the child controls](http://www.wpf-tutorial.com/chapters/panels/images/dockpanel_widths_heights.png "A DockPanel where width or heights has been specified for the child controls")

+++

The top and bottom controls now take precedence over the left and right controls, and they're all taking up 50 pixels in either height or width. If you make the window bigger or smaller, you will also see that this static width/height remains the same no matter what - only the center area increases or decreases in size as you resize the window.

+++

## LastChildFill

As already mentioned, the default behavior is that the last child of the DockPanel takes up the rest of the space, but this can be disabled using the LastChildFill. Here's an example where we disable it, and at the same time we'll show the ability to dock more than one control to the same side:

+++

```XML
<Window x:Class="WpfTutorialSamples.Panels.DockPanel"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DockPanel" Height="300" Width="300">
        <DockPanel LastChildFill="False">
                <Button DockPanel.Dock="Top" Height="50">Top</Button>
                <Button DockPanel.Dock="Bottom" Height="50">Bottom</Button>
                <Button DockPanel.Dock="Left" Width="50">Left</Button>
                <Button DockPanel.Dock="Left" Width="50">Left</Button>
                <Button DockPanel.Dock="Right" Width="50">Right</Button>
                <Button DockPanel.Dock="Right" Width="50">Right</Button>
        </DockPanel>
</Window>
```

+++

![A DockPanel where the LastChildFill property has been disabled](http://www.wpf-tutorial.com/chapters/panels/images/dockpanel_last_child_fill_disabled.png "A DockPanel where the LastChildFill property has been disabled")

In this example, we dock two controls to the left and two controls to the right, and at the same time, we turn off the LastChildFill property. This leaves us with empty space in the center, which may be preferable in some cases.

---

# The Grid Control

The Grid is probably the most complex of the panel types. A Grid can contain multiple rows and columns. You define a height for each of the rows and a width for each of the columns, in either an absolute amount of pixels, in a percentage of the available space or as auto, where the row or column will automatically adjust its size depending on the content. Use the Grid when the other panels doesn't do the job, e.g. when you need multiple columns and often in combination with the other panels.

+++

In its most basic form, the Grid will simply take all of the controls you put into it, stretch them to use the maximum available space and place it on top of each other:

```XML
Download sample
<Window x:Class="WpfTutorialSamples.Panels.Grid"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Grid" Height="300" Width="300">
    <Grid>
                <Button>Button 1</Button>
                <Button>Button 2</Button>
        </Grid>
</Window>
```

+++

![A simple Grid](http://www.wpf-tutorial.com/chapters/panels/images/grid_simple.png "A simple Grid")

+++

As you can see, the last control gets the top position, which in this case means that you can't even see the first button. Not terribly useful for most situations though, so let's try dividing the space, which is what the grid does so well. We do that by using ColumnDefinitions and RowDefinitions. In the first example, we'll stick to columns:

+++

```XML
<Window x:Class="WpfTutorialSamples.Panels.Grid"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Grid" Height="300" Width="300">
    <Grid>
                <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                </Grid.ColumnDefinitions>
                <Button>Button 1</Button>
                <Button Grid.Column="1">Button 2</Button>
        </Grid>
</Window>
```

+++

![A Grid divided into two columns](http://www.wpf-tutorial.com/chapters/panels/images/grid_two_columns.png "A Grid divided into two columns")

+++

In this example, we have simply divided the available space into two columns, which will share the space equally, using a "star width" (this will be explained later). On the second button, I use a so-called Attached property to place the button in the second column (0 is the first column, 1 is the second and so on). I could have used this property on the first button as well, but it automatically gets assigned to the first column and the first row, which is exactly what we want here.

+++

As you can see, the controls take up all the available space, which is the default behavior when the grid arranges its child controls. It does this by setting the HorizontalAlignment and VerticalAlignment on its child controls to Stretch.

In some situations you may want them to only take up the space they need though and/or control how they are placed in the Grid. The easiest way to do this is to set the HorizontalAlignment and VerticalAlignment directly on the controls you wish to manipulate. Here's a modified version of the above example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Panels.Grid"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Grid" Height="300" Width="300">
    <Grid>
                <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                </Grid.ColumnDefinitions>               
                <Button VerticalAlignment="Top" HorizontalAlignment="Center">Button 1</Button>
                <Button Grid.Column="1" VerticalAlignment="Center" HorizontalAlignment="Right">Button 2</Button>
        </Grid>
</Window>
```

+++

![A Grid divided into two columns with custom alignment on the child controls](http://www.wpf-tutorial.com/chapters/panels/images/grid_two_columns_alignment.png "A Grid divided into two columns with custom alignment on the child controls")

As you can see from the resulting screenshot, the first button is now placed in the top and centered. The second button is placed in the middle, aligned to the right.

---

# The Grid - Rows & columns

In the last chapter, we introduced you to the great Grid panel and showed you a couple of basic examples on how to use it. In this chapter we will do some more advanced layouts, as this is where the Grid really shines. First of all, let's throw in more columns and even some rows, for a true tabular layout:

+++

```XML
<Window x:Class="WpfTutorialSamples.Panels.TabularGrid"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TabularGrid" Height="300" Width="300">
    <Grid>
                <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="2*" />
                        <ColumnDefinition Width="1*" />
                        <ColumnDefinition Width="1*" />
                </Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                        <RowDefinition Height="2*" />
                        <RowDefinition Height="1*" />
                        <RowDefinition Height="1*" />
                </Grid.RowDefinitions>
                <Button>Button 1</Button>
                <Button Grid.Column="1">Button 2</Button>
                <Button Grid.Column="2">Button 3</Button>
                <Button Grid.Row="1">Button 4</Button>
                <Button Grid.Column="1" Grid.Row="1">Button 5</Button>
                <Button Grid.Column="2" Grid.Row="1">Button 6</Button>
                <Button Grid.Row="2">Button 7</Button>
                <Button Grid.Column="1" Grid.Row="2">Button 8</Button>
                <Button Grid.Column="2" Grid.Row="2">Button 9</Button>
        </Grid>
</Window>
```

+++

![A Grid with several columns and rows, creating a tabular layout](http://www.wpf-tutorial.com/chapters/panels/images/grid_tabular.png "A Grid with several columns and rows, creating a tabular layout")

A total of nine buttons, each placed in their own cell in a grid containing three rows and three columns. 

+++

We once again use a star based width, but this time we assign a number as well - the first row and the first column has a width of 2*, which basically means that it uses twice the amount of space as the rows and columns with a width of 1* (or just * - that's the same).

+++

You will also notice that I use the Attached properties Grid.Row and Grid.Column to place the controls in the grid, and once again you will notice that I have omitted these properties on the controls where I want to use either the first row or the first column (or both). This is essentially the same as specifying a zero. This saves a bit of typing, but you might prefer to assign them anyway for a better overview - that's totally up to you!

---

# The Grid - Units

So far we have mostly used the star width/height, which specifies that a row or a column should take up a certain percentage of the combined space. However, there are two other ways of specifying the width or height of a column or a row: Absolute units and the Auto width/height. Let's try creating a Grid where we mix these:

+++

```XML
<Window x:Class="WpfTutorialSamples.Panels.GridUnits"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="GridUnits" Height="200" Width="400">
        <Grid>
                <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="1*" />
                        <ColumnDefinition Width="Auto" />
                        <ColumnDefinition Width="100" />
                </Grid.ColumnDefinitions>
                <Button>Button 1</Button>
                <Button Grid.Column="1">Button 2 with long text</Button>
                <Button Grid.Column="2">Button 3</Button>
        </Grid>
</Window>
```

+++

![A Grid with columns of varying widths](http://www.wpf-tutorial.com/chapters/panels/images/grid_units_simple.png "A Grid with columns of varying widths")

In this example, the first button has a star width, the second one has its width set to Auto and the last one has a static width of 100 pixels.

+++

The result can be seen on the screenshot, where the second button only takes exactly the amount of space it needs to render its longer text, the third button takes exactly the 100 pixels it was promised and the first button, with the variable width, takes the rest.

+++

In a Grid where one or several columns (or rows) have a variable (star) width, they automatically get to share the width/height not already used by the columns/rows which uses an absolute or Auto width/height. This becomes more obvious when we resize the window:

+++

![A Grid with columns of varying widths, resized to a smaller size](http://www.wpf-tutorial.com/chapters/panels/images/grid_units_small.png "A Grid with columns of varying widths, resized to a smaller size") 

+++

![A Grid with columns of varying widths, resized to a larger size](http://www.wpf-tutorial.com/chapters/panels/images/grid_units_large.png "A Grid with columns of varying widths, resized to a larger size")

+++

On the first screenshot, you will see that the Grid reserves the space for the last two buttons, even though it means that the first one doesn't get all the space it needs to render properly. On the second screenshot, you will see the last two buttons keeping the exact same amount of space, leaving the surplus space to the first button.

+++

This can be a very useful technique when designing a wide range of dialogs. For instance, consider a simple contact form where the user enters a name, an e-mail address and a comment. The first two fields will usually have a fixed height, while the last one might as well take up as much space as possible, leaving room to type a longer comment. In the next chapter, we will try building a contact form, using the grid and rows and columns of different heights and widths.

---

# The Grid - Spanning

The default Grid behavior is that each control takes up one cell, but sometimes you want a certain control to take up more rows or columns. Fortunately the Grid makes this very easy, with the Attached properties ColumnSpan and RowSpan. The default value for this property is obviously 1, but you can specify a bigger number to make the control span more rows or columns.

+++

Here's a very simple example, where we use the ColumnSpan property:

```XML
<Window x:Class="WpfTutorialSamples.Panels.GridColRowSpan"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="GridColRowSpan" Height="110" Width="300">
        <Grid>
                <Grid.ColumnDefinitions>                        
                        <ColumnDefinition Width="1*" />
                        <ColumnDefinition Width="1*" />
                </Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                        <RowDefinition Height="*" />
                        <RowDefinition Height="*" />
                </Grid.RowDefinitions>
                <Button>Button 1</Button>
                <Button Grid.Column="1">Button 2</Button>
                <Button Grid.Row="1" Grid.ColumnSpan="2">Button 3</Button>
        </Grid>
</Window>
```

+++

![A Grid with column spanning applied to one of the controls](http://www.wpf-tutorial.com/chapters/panels/images/grid_col_span.png "A Grid with column spanning applied to one of the controls")

+++

We just define two columns and two rows, all of them taking up their equal share of the place. The first two buttons just use the columns normally, but with the third button, we make it take up two columns of space on the second row, using the ColumnSpan attribute.

This is all so simple that we could have just used a combination of panels to achieve the same effect, but for just slightly more advanced cases, this is really useful. Let's try something which better shows how powerful this is:

+++

```XML
Download sample
<Window x:Class="WpfTutorialSamples.Panels.GridColRowSpanAdvanced"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="GridColRowSpanAdvanced" Height="300" Width="300">
    <Grid>
                <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                </Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                        <RowDefinition Height="*" />
                        <RowDefinition Height="*" />
                        <RowDefinition Height="*" />
                </Grid.RowDefinitions>
                <Button Grid.ColumnSpan="2">Button 1</Button>
                <Button Grid.Column="3">Button 2</Button>
                <Button Grid.Row="1">Button 3</Button>
                <Button Grid.Column="1" Grid.Row="1" Grid.RowSpan="2" Grid.ColumnSpan="2">Button 4</Button>
                <Button Grid.Column="0" Grid.Row="2">Button 5</Button>
        </Grid>
</Window>
```

+++

![A Grid with both column and row spanning applied to several child controls](http://www.wpf-tutorial.com/chapters/panels/images/grid_col_row_span.png "A Grid with both column and row spanning applied to several child controls")

+++

With three columns and three rows we would normally have nine cells, but in this example, we use a combination of row and column spanning to fill all the available space with just five buttons. As you can see, a control can span either extra columns, extra rows or in the case of button 4: both.

So as you can see, spanning multiple columns and/or rows in a Grid is very easy. In a later article, we will use the spanning, along with all the other Grid techniques in a more practical example.

---


# The GridSplitter

As you saw in the previous articles, the Grid panel makes it very easy to divide up the available space into individual cells. Using column and row definitions, you can easily decide how much space each row or column should take up, but what if you want to allow the user to change this? This is where the GridSplitter control comes into play.

+++

The GridSplitter is used simply by adding it to a column or a row in a Grid, with the proper amount of space for it, e.g. 5 pixels. It will then allow the user to drag it from side to side or up and down, while changing the size of the column or row on each of the sides of it. Here's an example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Panels.GridSplitterSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="GridSplitterSample" Height="300" Width="300">
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="5" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>
        <TextBlock FontSize="55" HorizontalAlignment="Center" VerticalAlignment="Center" TextWrapping="Wrap">Left side</TextBlock>
        <GridSplitter Grid.Column="1" Width="5" HorizontalAlignment="Stretch" />
        <TextBlock Grid.Column="2" FontSize="55" HorizontalAlignment="Center" VerticalAlignment="Center" TextWrapping="Wrap">Right side</TextBlock>
    </Grid>
</Window>
```

+++

![A Grid panel with a GridSplitter control](http://www.wpf-tutorial.com/chapters/panels/images/grid_splitter_vertical_centered.png "A Grid panel with a GridSplitter control")

+++

![A Grid panel with a GridSplitter control in action](http://www.wpf-tutorial.com/chapters/panels/images/grid_splitter_vertical_not_centered.png "A Grid panel with a GridSplitter control in action")

+++

As you can see, I've simply created a Grid with two equally wide columns, with a 5 pixel column in the middle. Each of the sides are just a TextBlock control to illustrate the point. As you can see from the screenshots, the GridSplitter is rendered as a dividing line between the two columns and as soon as the mouse is over it, the cursor is changed to reflect that it can be resized.

+++

## Horizontal GridSplitter

The GridSplitter is very easy to use and of course it supports horizontal splits as well. In fact, you hardly have to change anything to make it work horizontally instead of vertically, as the next example will show:

+++

```XML
<Window x:Class="WpfTutorialSamples.Panels.GridSplitterHorizontalSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="GridSplitterHorizontalSample" Height="300" Width="300">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
            <RowDefinition Height="5" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        <TextBlock FontSize="55" HorizontalAlignment="Center" VerticalAlignment="Center" TextWrapping="Wrap">Top</TextBlock>
        <GridSplitter Grid.Row="1" Height="5" HorizontalAlignment="Stretch" />
        <TextBlock Grid.Row="2" FontSize="55" HorizontalAlignment="Center" VerticalAlignment="Center" TextWrapping="Wrap">Bottom</TextBlock>
    </Grid>
</Window>
```

+++

![A horizontal Grid panel with a GridSplitter control in action](http://www.wpf-tutorial.com/chapters/panels/images/grid_splitter_horizontal_not_centered.png "A horizontal Grid panel with a GridSplitter control in action")

As you can see, I simply changed the columns into rows and on the GridSplitter, I defined a Height instead of a Width. The GridSplitter figures out the rest on its own, but in case it doesn't, you can use the **ResizeDirection** property on it to force it into either Rows or Columns mode.

---

# Using the Grid: A contact form

In the last couple of chapters we went through a lot of theoretic information, each with some very theoretic examples. In this chapter we will combine what we have learned about the Grid so far, into an example that can be used in the real world: A simple contact form.

+++

The good thing about the contact form is that it's just an example of a commonly used dialog - you can take the techniques used and apply them to almost any type of dialog that you need to create.

The first take on this task is very simple and will show you a very basic contact form. It uses three rows, two of them with Auto heights and the last one with star height, so it consumes the rest of the available space:

+++

```XML
<Window x:Class="WpfTutorialSamples.Panels.GridContactForm"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="GridContactForm" Height="300" Width="300">
    <Grid>
                <Grid.RowDefinitions>
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="*" />
                </Grid.RowDefinitions>          
                <TextBox>Name</TextBox>
                <TextBox Grid.Row="1">E-mail</TextBox>
                <TextBox Grid.Row="2" AcceptsReturn="True">Comment</TextBox>            
        </Grid>
</Window>
```

+++

![A simple contact form using the Grid](http://www.wpf-tutorial.com/chapters/panels/images/grid_contact_form_simple.png "A simple contact form using the Grid")

As you can see, the last TextBox simply takes up the remaining space, while the first two only takes up the space they require. Try resizing the window and you will see the comment TextBox resize with it.

+++

In this very simple example, there are no labels to designate what each of the fields are for. Instead, the explanatory text is inside the TextBox, but this is not generally how a Windows dialog looks. Let's try improving the look and usability a bit:

+++

```XML
<Window x:Class="WpfTutorialSamples.Panels.GridContactFormTake2"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="GridContactFormTake2" Height="300" Width="300">
        <Grid Margin="10">
                <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="Auto" />
                        <ColumnDefinition Width="*" />
                </Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="Auto" />
                        <RowDefinition Height="*" />
                </Grid.RowDefinitions>
                <Label>Name: </Label>
                <TextBox Grid.Column="1" Margin="0,0,0,10" />
                <Label Grid.Row="1">E-mail: </Label>
                <TextBox Grid.Row="1" Grid.Column="1" Margin="0,0,0,10" />
                <Label Grid.Row="2">Comment: </Label>
                <TextBox Grid.Row="2" Grid.Column="1" AcceptsReturn="True" />
        </Grid>
</Window>
```

+++

![A simple contact form using the Grid - take two](http://www.wpf-tutorial.com/chapters/panels/images/grid_contact_form_take2.png "A simple contact form using the Grid - take two")

+++

But perhaps you're in a situation where the comment field is pretty self-explanatory? In that case, let's skip the label and use ColumnSpan to get even more space for the comment TextBox:

```XML
<TextBox Grid.ColumnSpan="2" Grid.Row="2" AcceptsReturn="True" />
```

+++

![A simple contact form using the Grid - take three](http://www.wpf-tutorial.com/chapters/panels/images/grid_contact_form_take3.png "A simple contact form using the Grid - take three")

So as you can see, the Grid is a very powerful panel. Hopefully you can use all of these techniques when designing your own dialogs.
