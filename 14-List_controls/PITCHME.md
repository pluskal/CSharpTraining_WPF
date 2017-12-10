# The ItemsControl

WPF has a wide range of controls for displaying a list of data. They come in several shapes and forms and vary in how complex they are and how much work they perform for you. The simplest variant is the ItemsControl, which is pretty much just a markup-based loop - you need to apply all the styling and templating, but in many cases, that's just what you need.

+++

## A simple ItemsControl example

Let's kick off with a very simple example, where we hand-feed the ItemsControl with a set of items. This should show you just how simple the ItemsControl is:

+++

```XML
<Window x:Class="WpfTutorialSamples.ItemsControl.ItemsControlSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:system="clr-namespace:System;assembly=mscorlib"
        Title="ItemsControlSample" Height="150" Width="200">
    <Grid Margin="10">
                <ItemsControl>
                        <system:String>ItemsControl Item #1</system:String>
                        <system:String>ItemsControl Item #2</system:String>
                        <system:String>ItemsControl Item #3</system:String>
                        <system:String>ItemsControl Item #4</system:String>
                        <system:String>ItemsControl Item #5</system:String>
                </ItemsControl>
        </Grid>
</Window>
```

+++

![A simple ItemsControl with items defined in the markup](http://www.wpf-tutorial.com/chapters/list-controls/images/itemscontrol_simple.png "A simple ItemsControl with items defined in the markup")

+++

As you can see, there is nothing that shows that we're using a control for repeating the items instead of just manually adding e.g. 5 TextBlock controls - the ItemsControl is completely lookless by default. If you click on one of the items, nothing happens, because there's no concept of selected item(s) or anything like that.

+++

## ItemsControl with data binding

Of course the ItemsControl is not meant to be used with items defined in the markup, like we did in the first example. Like pretty much any other control in WPF, the ItemsControl is made for data binding, where we use a template to define how our code-behind classes should be presented to the user.

+++

To demonstrate that, I've whipped up an example where we display a TODO list to the user, and to show you just how flexible everything gets once you define your own templates, I've used a ProgressBar control to show you the current completion percentage. First some code, then a screenshot and then an explanation of it all:

+++


```XML
<Window x:Class="WpfTutorialSamples.ItemsControl.ItemsControlDataBindingSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ItemsControlDataBindingSample" Height="150" Width="300">
    <Grid Margin="10">
                <ItemsControl Name="icTodoList">
                        <ItemsControl.ItemTemplate>
                                <DataTemplate>
                                        <Grid Margin="0,0,0,5">
                                                <Grid.ColumnDefinitions>
                                                        <ColumnDefinition Width="*" />
                                                        <ColumnDefinition Width="100" />
                                                </Grid.ColumnDefinitions>
                                                <TextBlock Text="{Binding Title}" />
                                                <ProgressBar Grid.Column="1" Minimum="0" Maximum="100" Value="{Binding Completion}" />
                                        </Grid>
                                </DataTemplate>
                        </ItemsControl.ItemTemplate>
                </ItemsControl>
        </Grid>
</Window>
```

+++

```C#
using System;
using System.Windows;
using System.Collections.Generic;

namespace WpfTutorialSamples.ItemsControl
{
        public partial class ItemsControlDataBindingSample : Window
        {
                public ItemsControlDataBindingSample()
                {
                        InitializeComponent();

                        List<TodoItem> items = new List<TodoItem>();
                        items.Add(new TodoItem() { Title = "Complete this WPF tutorial", Completion = 45 });
                        items.Add(new TodoItem() { Title = "Learn C#", Completion = 80 });
                        items.Add(new TodoItem() { Title = "Wash the car", Completion = 0 });

                        icTodoList.ItemsSource = items;
                }
        }

        public class TodoItem
        {
                public string Title { get; set; }
                public int Completion { get; set; }
        }
}
```

+++

![An ItemsControl using data binding](http://www.wpf-tutorial.com/chapters/list-controls/images/itemscontrol_data_binding.png "An ItemsControl using data binding")

+++

The most important part of this example is the template that we specify inside of the ItemsControl, using a DataTemplate tag inside of the ItemsControl.ItemTemplate. We add a Grid panel, to get two columns: In the first we have a TextBlock, which will show the title of the TODO item, and in the second column we have a ProgressBar control, which value we bind to the Completion property.

+++

The template now represents a TodoItem, which we declare in the Code-behind file, where we also instantiate a number of them and add them to a list. In the end, this list is assigned to the **ItemsSource** property of our ItemsControl, which then does the rest of the job for us. Each item in the list is displayed by using our template, as you can see from the resulting screenshot.

+++

## The ItemsPanelTemplate property

In the above examples, all items are rendered from top to bottom, with each item taking up the full row. This happens because the ItemsControl throw all of our items into a vertically aligned StackPanel by default. It's very easy to change though, since the ItemsControl allows you to change which panel type is used to hold all the items. Here's an example:

+++

```XML
<Window x:Class="WpfTutorialSamples.ItemsControl.ItemsControlPanelSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:system="clr-namespace:System;assembly=mscorlib"
        Title="ItemsControlPanelSample" Height="150" Width="250">
        <Grid Margin="10">
                <ItemsControl>
                        <ItemsControl.ItemsPanel>
                                <ItemsPanelTemplate>
                                        <WrapPanel />
                                </ItemsPanelTemplate>
                        </ItemsControl.ItemsPanel>
                        <ItemsControl.ItemTemplate>
                                <DataTemplate>
                                        <Button Content="{Binding}" Margin="0,0,5,5" />
                                </DataTemplate>
                        </ItemsControl.ItemTemplate>
                        <system:String>Item #1</system:String>
                        <system:String>Item #2</system:String>
                        <system:String>Item #3</system:String>
                        <system:String>Item #4</system:String>
                        <system:String>Item #5</system:String>
                </ItemsControl>
        </Grid>
</Window>
```

+++

![An ItemsControl using a WrapPanel for holding the items](http://www.wpf-tutorial.com/chapters/list-controls/images/itemscontrol_wrappanel.png "An ItemsControl using a WrapPanel for holding the items")

+++

We specify that the ItemsControl should use a WrapPanel as its template by declaring one in the **ItemsPanelTemplate** property and just for fun, we throw in an ItemTemplate that causes the strings to be rendered as buttons. You can use any of the WPF panels, but some are more useful than others.

+++

Another good example is the UniformGrid panel, where we can define a number of columns and then have our items neatly shown in equally-wide columns:

```XML
<Window x:Class="WpfTutorialSamples.ItemsControl.ItemsControlPanelSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:system="clr-namespace:System;assembly=mscorlib"
        Title="ItemsControlPanelSample" Height="150" Width="250">
        <Grid Margin="10">
                <ItemsControl>
                        <ItemsControl.ItemsPanel>
                                <ItemsPanelTemplate>
                                        <UniformGrid Columns="2" />
                                </ItemsPanelTemplate>
                        </ItemsControl.ItemsPanel>
                        <ItemsControl.ItemTemplate>
                                <DataTemplate>
                                        <Button Content="{Binding}" Margin="0,0,5,5" />
                                </DataTemplate>
                        </ItemsControl.ItemTemplate>
                        <system:String>Item #1</system:String>
                        <system:String>Item #2</system:String>
                        <system:String>Item #3</system:String>
                        <system:String>Item #4</system:String>
                        <system:String>Item #5</system:String>
                </ItemsControl>
        </Grid>
</Window>
```

+++

![An ItemsControl using a UniformGrid panel for holding the items](http://www.wpf-tutorial.com/chapters/list-controls/images/itemscontrol_wrappanel.png "An ItemsControl using a UniformGrid panel for holding the items")

+++

## ItemsControl with scrollbars

Once you start using the ItemsControl, you might run into a very common problem: By default, the ItemsControl doesn't have any scrollbars, which means that if the content doesn't fit, it's just clipped. This can be seen by taking our first example from this article and resizing the window:

+++

![An ItemsControl without scrollbars](http://www.wpf-tutorial.com/chapters/list-controls/images/itemscontrol_clipped.png "An ItemsControl without scrollbars")

+++

WPF makes this very easy to solve though. There are a number of possible solutions, for instance you can alter the template used by the ItemsControl to include a ScrollViewer control, but the easiest solution is to simply throw a ScrollViewer around the ItemsControl. Here's an example:

+++

```XML
<Window x:Class="WpfTutorialSamples.ItemsControl.ItemsControlSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:system="clr-namespace:System;assembly=mscorlib"
        Title="ItemsControlSample" Height="150" Width="200">
        <Grid Margin="10">
                <ScrollViewer VerticalScrollBarVisibility="Auto" HorizontalScrollBarVisibility="Auto">
                        <ItemsControl>
                                <system:String>ItemsControl Item #1</system:String>
                                <system:String>ItemsControl Item #2</system:String>
                                <system:String>ItemsControl Item #3</system:String>
                                <system:String>ItemsControl Item #4</system:String>
                                <system:String>ItemsControl Item #5</system:String>
                        </ItemsControl>
                </ScrollViewer>
        </Grid>
</Window>
```

+++

![An ItemsControl without scrollbars](http://www.wpf-tutorial.com/chapters/list-controls/images/itemscontrol_scrollviewer.png "An ItemsControl without scrollbars")

I set the two visibility options to Auto, to make them only visible when needed. As you can see from the screenshot, you can now scroll through the list of items.

+++

## Summary

The ItemsControl is great when you want full control of how your data is displayed, and when you don't need any of your content to be selectable. If you want the user to be able to select items from the list, then you're better off with one of the other controls, e.g. the ListBox or the ListView. They will be described in upcoming chapters.

---

# The ListBox control

In the last article, we had a look at the ItemsControl, which is probably the simplest list in WPF. The ListBox control is the next control in line, which adds a bit more functionality. One of the main differences is the fact that the ListBox control actually deals with selections, allowing the end-user to select one or several items from the list and automatically giving visual feedback for it.

+++

Here's an example of a very simple ListBox control:

```XML
<Window x:Class="WpfTutorialSamples.ListBox_control.ListBoxSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListBoxSample" Height="120" Width="200">
    <Grid Margin="10">
                <ListBox>
                        <ListBoxItem>ListBox Item #1</ListBoxItem>
                        <ListBoxItem>ListBox Item #2</ListBoxItem>
                        <ListBoxItem>ListBox Item #3</ListBoxItem>
                </ListBox>
        </Grid>
</Window>
```

+++

![A simple ListBox control with items defined in the markup](http://www.wpf-tutorial.com/chapters/list-controls/images/listbox_simple.png "A simple ListBox control with items defined in the markup")

This is as simple as it gets: We declare a ListBox control, and inside of it, we declare three ListBoxItem's, each with its own text. However, since the ListBoxItem is actually a ContentControl, we can define custom content for it:

+++

```XML
<Window x:Class="WpfTutorialSamples.ListBox_control.ListBoxSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListBoxSample" Height="120" Width="200">
        <Grid Margin="10">
                <ListBox>
                        <ListBoxItem>
                                <StackPanel Orientation="Horizontal">
                                        <Image Source="/WpfTutorialSamples;component/Images/bullet_blue.png" />
                                        <TextBlock>ListBox Item #1</TextBlock>
                                </StackPanel>
                        </ListBoxItem>
                        <ListBoxItem>
                                <StackPanel Orientation="Horizontal">
                                        <Image Source="/WpfTutorialSamples;component/Images/bullet_green.png" />
                                        <TextBlock>ListBox Item #2</TextBlock>
                                </StackPanel>
                        </ListBoxItem>
                        <ListBoxItem>
                                <StackPanel Orientation="Horizontal">
                                        <Image Source="/WpfTutorialSamples;component/Images/bullet_red.png" />
                                        <TextBlock>ListBox Item #3</TextBlock>
                                </StackPanel>
                        </ListBoxItem>
                </ListBox>
        </Grid>
</Window>
```

+++

![A ListBox control with custom content](http://www.wpf-tutorial.com/chapters/list-controls/images/listbox_custom_content.png "A ListBox control with custom content")

+++

For each of the ListBoxItem's we now add a StackPanel, in which we add an Image and a TextBlock. This gives us full control of the content as well as the text rendering, as you can see from the screenshot, where different colors have been used for each of the numbers.

From the screenshot you might also notice another difference when comparing the ItemsControl to the ListBox: By default, a border is shown around the control, making it look like an actual control instead of just output.

+++

## Data binding the ListBox

Manually defining items for the ListBox makes for a fine first example, but most of the times, your ListBox controls will be filled with items from a data source using data binding. By default, if you bind a list of items to the ListBox, their ToString() method will be used to represent each item. This is rarely what you want, but fortunately, we can easily declare a template that will be used to render each item.

+++

I have re-used the TODO based example from the ItemsControl article, where we build a cool TODO list using a simple Code-behind class and, in this case, a ListBox control for the visual representation. Here's the example:

```XML
<Window x:Class="WpfTutorialSamples.ListBox_control.ListBoxDataBindingSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListBoxDataBindingSample" Height="150" Width="300">
    <Grid Margin="10">
                <ListBox Name="lbTodoList" HorizontalContentAlignment="Stretch">
                        <ListBox.ItemTemplate>
                                <DataTemplate>
                                        <Grid Margin="0,2">
                                                <Grid.ColumnDefinitions>
                                                        <ColumnDefinition Width="*" />
                                                        <ColumnDefinition Width="100" />
                                                </Grid.ColumnDefinitions>
                                                <TextBlock Text="{Binding Title}" />
                                                <ProgressBar Grid.Column="1" Minimum="0" Maximum="100" Value="{Binding Completion}" />
                                        </Grid>
                                </DataTemplate>
                        </ListBox.ItemTemplate>
                </ListBox>
        </Grid>
</Window>
```

+++

```C#
using System;
using System.Windows;
using System.Collections.Generic;

namespace WpfTutorialSamples.ListBox_control
{
        public partial class ListBoxDataBindingSample : Window
        {
                public ListBoxDataBindingSample()
                {
                        InitializeComponent();
                        List<TodoItem> items = new List<TodoItem>();
                        items.Add(new TodoItem() { Title = "Complete this WPF tutorial", Completion = 45 });
                        items.Add(new TodoItem() { Title = "Learn C#", Completion = 80 });
                        items.Add(new TodoItem() { Title = "Wash the car", Completion = 0 });

                        lbTodoList.ItemsSource = items;
                }
        }

        public class TodoItem
        {
                public string Title { get; set; }
                public int Completion { get; set; }
        }
}
```

+++

![A ListBox control using data binding](http://www.wpf-tutorial.com/chapters/list-controls/images/listbox_databinding.png "A ListBox control using data binding")

+++

All the magic happens in the ItemTemplate that we have defined for the ListBox. In there, we specify that each ListBox item should consist of a Grid, divided into two columns, with a TextBlock showing the title in the first and a ProgressBar showing the completion status in the second column. To get the values out, we use some very simple data binding, which is all explained in the data binding part of this tutorial.

+++

In the Code-behind file, we have declared a very simple TodoItem class to hold each of our TODO items. In the constructor of the window, we initialize a list, add three TODO items to it and then assign it to the ItemsSource of the ListBox. The combination of the ItemsSource and the ItemTemplate we specified in the XAML part, this is all WPF need to render all of the items as a TODO list.

+++

Please notice the **HorizontalContentAlignment** property that I set to **Stretch** on the ListBox. The default content alignment for a ListBox item is **Left**, which means that each item only takes up as much horizontal space as it needs. The result? Well, not quite what we want:

+++

![A ListBox control where the HorizontalContentAlignment property has not been changed](http://www.wpf-tutorial.com/chapters/list-controls/images/listbox_content_alignment_bad.png "A ListBox control where the HorizontalContentAlignment property has not been changed")

By using the Stretch alignment, each item is stretched to take up the full amount of available space, as you can see from the previous screenshot.

+++

## Working with ListBox selection

As mentioned, a key difference between the ItemsControl and the ListBox is that the ListBox handles and displays user selection for you. Therefore, a lot of ListBox question revolves around somehow working with the selection. To help with some of these questions, I have created a bigger example, showing you some selection related tricks:

+++

```XML
<Window x:Class="WpfTutorialSamples.ListBox_control.ListBoxSelectionSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListBoxSelectionSample" Height="250" Width="450">
        <DockPanel Margin="10">
                <StackPanel DockPanel.Dock="Right" Margin="10,0">
                        <StackPanel.Resources>
                                <Style TargetType="Button">
                                        <Setter Property="Margin" Value="0,0,0,5" />
                                </Style>
                        </StackPanel.Resources>
                        <TextBlock FontWeight="Bold" Margin="0,0,0,10">ListBox selection</TextBlock>
                        <Button Name="btnShowSelectedItem" Click="btnShowSelectedItem_Click">Show selected</Button>
                        <Button Name="btnSelectLast" Click="btnSelectLast_Click">Select last</Button>
                        <Button Name="btnSelectNext" Click="btnSelectNext_Click">Select next</Button>
                        <Button Name="btnSelectCSharp" Click="btnSelectCSharp_Click">Select C#</Button>
                        <Button Name="btnSelectAll" Click="btnSelectAll_Click">Select all</Button>
                </StackPanel>
                <ListBox Name="lbTodoList" HorizontalContentAlignment="Stretch" SelectionMode="Extended" SelectionChanged="lbTodoList_SelectionChanged">
                        <ListBox.ItemTemplate>
                                <DataTemplate>
                                        <Grid Margin="0,2">
                                                <Grid.ColumnDefinitions>
                                                        <ColumnDefinition Width="*" />
                                                        <ColumnDefinition Width="100" />
                                                </Grid.ColumnDefinitions>
                                                <TextBlock Text="{Binding Title}" />
                                                <ProgressBar Grid.Column="1" Minimum="0" Maximum="100" Value="{Binding Completion}" />
                                        </Grid>
                                </DataTemplate>
                        </ListBox.ItemTemplate>
                </ListBox>
        </DockPanel>
</Window>
```

+++

```C#
using System;
using System.Windows;
using System.Collections.Generic;

namespace WpfTutorialSamples.ListBox_control
{
        public partial class ListBoxSelectionSample : Window
        {
                public ListBoxSelectionSample()
                {
                        InitializeComponent();
                        List<TodoItem> items = new List<TodoItem>();
                        items.Add(new TodoItem() { Title = "Complete this WPF tutorial", Completion = 45 });
                        items.Add(new TodoItem() { Title = "Learn C#", Completion = 80 });
                        items.Add(new TodoItem() { Title = "Wash the car", Completion = 0 });

                        lbTodoList.ItemsSource = items;
                }

                private void lbTodoList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
                {
                        if(lbTodoList.SelectedItem != null)
                                this.Title = (lbTodoList.SelectedItem as TodoItem).Title;
                }

                private void btnShowSelectedItem_Click(object sender, RoutedEventArgs e)
                {
                        foreach(object o in lbTodoList.SelectedItems)
                                MessageBox.Show((o as TodoItem).Title);
                }

                private void btnSelectLast_Click(object sender, RoutedEventArgs e)
                {
                        lbTodoList.SelectedIndex = lbTodoList.Items.Count - 1;
                }

                private void btnSelectNext_Click(object sender, RoutedEventArgs e)
                {
                        int nextIndex = 0;
                        if((lbTodoList.SelectedIndex >= 0) && (lbTodoList.SelectedIndex < (lbTodoList.Items.Count - 1)))
                                nextIndex = lbTodoList.SelectedIndex + 1;
                        lbTodoList.SelectedIndex = nextIndex;
                }

                private void btnSelectCSharp_Click(object sender, RoutedEventArgs e)
                {
                        foreach(object o in lbTodoList.Items)
                        {
                                if((o is TodoItem) && ((o as TodoItem).Title.Contains("C#")))
                                {
                                        lbTodoList.SelectedItem = o;
                                        break;
                                }
                        }
                }

                private void btnSelectAll_Click(object sender, RoutedEventArgs e)
                {
                        foreach(object o in lbTodoList.Items)
                                lbTodoList.SelectedItems.Add(o);
                }


        }

        public class TodoItem
        {
                public string Title { get; set; }
                public int Completion { get; set; }
        }
}
```

+++

![Working with selections in the ListBox control](http://www.wpf-tutorial.com/chapters/list-controls/images/listbox_selection.png "Working with selections in the ListBox control")

+++

As you can see, I have defined a range of buttons to the right of the ListBox, to either get or manipulate the selection. I've also changed the **SelectionMode** to **Extended**, to allow for the selection of multiple items. This can be done either programmatically, as I do in the example, or by the end-user, by holding down **[Ctrl]** or **[Shift]** while clicking on the items.

+++

For each of the buttons, I have defined a click handler in the Code-behind. Each action should be pretty self-explanatory and the C# code used is fairly simple, but if you're still in doubt, try running the example on your own machine and test out the various possibilities in the example.

+++

## Summary

The ListBox control is much like the ItemsControl and several of the same techniques can be used. The ListBox does offer a bit more functionality when compared to the ItemsControl, especially the selection handling. For even more functionality, like column headers, you should have a look at the ListView control, which is given a very thorough description later on in this tutorial with several articles explaining all the functionality.

---


# The ComboBox control

The ComboBox control is in many ways like the ListBox control, but takes up a lot less space, because the list of items is hidden when not needed. The ComboBox control is used many places in Windows, but to make sure that everyone knows how it looks and works, we'll jump straight into a simple example:

+++

```XML
<Window x:Class="WpfTutorialSamples.ComboBox_control.ComboBoxSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ComboBoxSample" Height="150" Width="200">
    <StackPanel Margin="10">
        <ComboBox>
            <ComboBoxItem>ComboBox Item #1</ComboBoxItem>
            <ComboBoxItem IsSelected="True">ComboBox Item #2</ComboBoxItem>
            <ComboBoxItem>ComboBox Item #3</ComboBoxItem>
        </ComboBox>
    </StackPanel>
</Window>
```

+++

![A simple ComboBox control](http://www.wpf-tutorial.com/chapters/list-controls/images/combobox_simple.png "A simple ComboBox control")

+++

In the screenshot, I have activated the control by clicking it, causing the list of items to be displayed. As you can see from the code, the ComboBox, in its simple form, is very easy to use. All I've done here is manually add some items, making one of them the default selected item by setting the IsSelected property on it.

+++

## Custom content

In the first example we only showed text in the items, which is pretty common for the ComboBox control, but since the ComboBoxItem is a ContentControl, we can actually use pretty much anything as content. Let's try making a slightly more sophisticated list of items:

+++

```XML
<Window x:Class="WpfTutorialSamples.ComboBox_control.ComboBoxCustomContentSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ComboBoxCustomContentSample" Height="150" Width="200">
    <StackPanel Margin="10">
        <ComboBox>
            <ComboBoxItem>
                <StackPanel Orientation="Horizontal">
                    <Image Source="/WpfTutorialSamples;component/Images/bullet_red.png" />
                    <TextBlock Foreground="Red">Red</TextBlock>
                </StackPanel>
            </ComboBoxItem>
            <ComboBoxItem>
                <StackPanel Orientation="Horizontal">
                    <Image Source="/WpfTutorialSamples;component/Images/bullet_green.png" />
                    <TextBlock Foreground="Green">Green</TextBlock>
                </StackPanel>
            </ComboBoxItem>
            <ComboBoxItem>
                <StackPanel Orientation="Horizontal">
                    <Image Source="/WpfTutorialSamples;component/Images/bullet_blue.png" />
                    <TextBlock Foreground="Blue">Blue</TextBlock>
                </StackPanel>
            </ComboBoxItem>
        </ComboBox>
    </StackPanel>
</Window>
```

+++

![A ComboBox control with custom content](http://www.wpf-tutorial.com/chapters/list-controls/images/combobox_custom_content.png "A ComboBox control with custom content")

For each of the ComboBoxItem's we now add a StackPanel, in which we add an Image and a TextBlock. This gives us full control of the content as well as the text rendering, as you can see from the screenshot, where both text color and image indicates a color value.

+++

## Data binding the ComboBox

As you can see from the first examples, manually defining the items of a ComboBox control is easy using XAML, but you will likely soon run into a situation where you need the items to come from some kind of data source, like a database or just an in-memory list. Using WPF data binding and a custom template, we can easily render a list of colors, including a preview of the color:

+++

```XML
<Window x:Class="WpfTutorialSamples.ComboBox_control.ComboBoxDataBindingSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ComboBoxDataBindingSample" Height="200" Width="200">
    <StackPanel Margin="10">
        <ComboBox Name="cmbColors">
            <ComboBox.ItemTemplate>
                <DataTemplate>
                    <StackPanel Orientation="Horizontal">
                        <Rectangle Fill="{Binding Name}" Width="16" Height="16" Margin="0,2,5,2" />
                        <TextBlock Text="{Binding Name}" />
                    </StackPanel>
                </DataTemplate>
            </ComboBox.ItemTemplate>
        </ComboBox>
    </StackPanel>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WpfTutorialSamples.ComboBox_control
{
        public partial class ComboBoxDataBindingSample : Window
        {
                public ComboBoxDataBindingSample()
                {
                        InitializeComponent();
                        cmbColors.ItemsSource = typeof(Colors).GetProperties();
                }
        }
}
```

+++

![A ComboBox control using data binding](http://www.wpf-tutorial.com/chapters/list-controls/images/combobox_data_binding.png "A ComboBox control using data binding")

+++

It's actually quite simple: In the Code-behind, I obtain a list of all the colors using a Reflection based approach with the Colors class. I assign it to the **ItemsSource** property of the ComboBox, which then renders each color using the template I have defined in the XAML part.

Each item, as defined by the ItemTemplate, consists of a StackPanel with a Rectangle and a TextBlock, each bound to the color value. This gives us a complete list of colors, with minimal effort - and it looks pretty good too, right?

## IsEditable

In the first examples, the user was only able to select from our list of items, but one of the cool things about the ComboBox is that it supports the possibility of letting the user both select from a list of items or enter their own value. This is extremely useful in situations where you want to help the user by giving them a pre-defined set of options, while still giving them the option to manually enter the desired value. This is all controlled by the **IsEditable** property, which changes the behavior and look of the ComboBox quite a bit:

+++

```XML
<Window x:Class="WpfTutorialSamples.ComboBox_control.ComboBoxEditableSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ComboBoxEditableSample" Height="150" Width="200">
    <StackPanel Margin="10">
        <ComboBox IsEditable="True">
            <ComboBoxItem>ComboBox Item #1</ComboBoxItem>
            <ComboBoxItem>ComboBox Item #2</ComboBoxItem>
            <ComboBoxItem>ComboBox Item #3</ComboBoxItem>
        </ComboBox>
    </StackPanel>
</Window>
```

+++

![An editable ComboBox control](http://www.wpf-tutorial.com/chapters/list-controls/images/combobox_editable.png "An editable ComboBox control")

As you can see, I can enter a completely different value or pick one from the list. If picked from the list, it simply overwrites the text of the ComboBox.

+++

As a lovely little bonus, the ComboBox will automatically try to help the user select an existing value when the user starts typing, as you can see from the next screenshot, where I just started typing "Co":

+++

![A ComboBox control with auto completion](http://www.wpf-tutorial.com/chapters/list-controls/images/combobox_auto_complete.png "A ComboBox control with auto completion")

By default, the matching is not case-sensitive but you can make it so by setting the **IsTextSearchCaseSensitive** to True. If you don't want this auto complete behavior at all, you can disable it by setting the **IsTextSearchEnabled** to False.

+++

## Working with ComboBox selection

A key part of using the ComboBox control is to be able to read the user selection, and even control it with code. In the next example, I've re-used the data bound ComboBox example, but added some buttons for controlling the selection. I've also used the **SelectionChanged** event to capture when the selected item is changed, either by code or by the user, and act on it.

+++

```XML
<Window x:Class="WpfTutorialSamples.ComboBox_control.ComboBoxSelectionSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ComboBoxSelectionSample" Height="125" Width="250">
    <StackPanel Margin="10">
        <ComboBox Name="cmbColors" SelectionChanged="cmbColors_SelectionChanged">
            <ComboBox.ItemTemplate>
                <DataTemplate>
                    <StackPanel Orientation="Horizontal">
                        <Rectangle Fill="{Binding Name}" Width="16" Height="16" Margin="0,2,5,2" />
                        <TextBlock Text="{Binding Name}" />
                    </StackPanel>
                </DataTemplate>
            </ComboBox.ItemTemplate>
        </ComboBox>
        <WrapPanel Margin="15" HorizontalAlignment="Center">
            <Button Name="btnPrevious" Click="btnPrevious_Click" Width="55">Previous</Button>
            <Button Name="btnNext" Click="btnNext_Click" Margin="5,0" Width="55">Next</Button>
            <Button Name="btnBlue" Click="btnBlue_Click" Width="55">Blue</Button>
        </WrapPanel>
    </StackPanel>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace WpfTutorialSamples.ComboBox_control
{
        public partial class ComboBoxSelectionSample : Window
        {
                public ComboBoxSelectionSample()
                {
                        InitializeComponent();
                        cmbColors.ItemsSource = typeof(Colors).GetProperties();
                }

                private void btnPrevious_Click(object sender, RoutedEventArgs e)
                {
                        if(cmbColors.SelectedIndex > 0)
                                cmbColors.SelectedIndex = cmbColors.SelectedIndex - 1;
                }

                private void btnNext_Click(object sender, RoutedEventArgs e)
                {
                        if(cmbColors.SelectedIndex < cmbColors.Items.Count-1)
                                cmbColors.SelectedIndex = cmbColors.SelectedIndex + 1;
                }

                private void btnBlue_Click(object sender, RoutedEventArgs e)
                {
                        cmbColors.SelectedItem = typeof(Colors).GetProperty("Blue");
                }

                private void cmbColors_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
                {
                        Color selectedColor = (Color)(cmbColors.SelectedItem as PropertyInfo).GetValue(null, null);
                        this.Background = new SolidColorBrush(selectedColor);
                }
        }
}
```

+++

![A ComboBox control where we work with the selection](http://www.wpf-tutorial.com/chapters/list-controls/images/combobox_selection.png "A ComboBox control where we work with the selection")

+++

The interesting part of this example is the three event handlers for our three buttons, as well as the **SelectionChanged** event handler. In the first two, we select the previous or the next item by reading the **SelectedIndex** property and then subtracting or adding one to it. Pretty simple and easy to work with.

+++

In the third event handler, we use the SelectedItem to select a specific item based on the value. I do a bit of extra work here (using .NET reflection), because the ComboBox is bound to a list of properties, each being a color, instead of a simple list of colors, but basically it's all about giving the value contained by one of the items to the **SelectedItem** property.

+++

In the fourth and last event handler, I respond to the selected item being changed. When that happens, I read the selected color (once again using Reflection, as described above) and then use the selected color to create a new background brush for the Window. The effect can be seen on the screenshot.

If you're working with an editable ComboBox (IsEditable property set to true), you can read the **Text** property to know the value the user has entered or selected.

---

# Acknowledgment

Material is based on [WPF-Tutorial](http://www.wpf-tutorial.com/).