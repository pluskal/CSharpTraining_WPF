# The DataGrid control

The DataGrid control looks a lot like the ListView, when using a GridView, but it offers a lot of additional functionality. For instance, the DataGrid can automatically generate columns, depending on the data you feed it with. The DataGrid is also editable by default, allowing the end-user to change the values of the underlying data source.

+++

The most common usage for the DataGrid is in combination with a database, but like most WPF controls, it works just as well with an in-memory source, like a list of objects. Since it's a lot easier to demonstrate, we'll mostly be using the latter approach in this tutorial.

+++

## A simple DataGrid

You can start using the DataGrid without setting any properties, because it supports so much out of the box. In this first example, we'll do just that, and then assign a list of our own User objects as the items source:

+++

```XML
<Window x:Class="WpfTutorialSamples.DataGrid_control.SimpleDataGridSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="SimpleDataGridSample" Height="180" Width="300">
    <Grid Margin="10">
                <DataGrid Name="dgSimple"></DataGrid>
        </Grid>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfTutorialSamples.DataGrid_control
{
        public partial class SimpleDataGridSample : Window
        {
                public SimpleDataGridSample()
                {
                        InitializeComponent();

                        List<User> users = new List<User>();
                        users.Add(new User() { Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23) });
                        users.Add(new User() { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17) });
                        users.Add(new User() { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2) });

                        dgSimple.ItemsSource = users;
                }
        }

        public class User
        {
                public int Id { get; set; }

                public string Name { get; set; }

                public DateTime Birthday { get; set; }
        }
}
```

+++

![A simple DataGrid control](http://www.wpf-tutorial.com/chapters/datagrid/images/datagrid_simple.png "A simple DataGrid control")

+++

That's really all you need to start using the DataGrid. The source could just as easily have been a database table/view or even an XML file - the DataGrid is not picky about where it gets its data from.

+++

If you click inside one of the cells, you can see that you're allowed to edit each of the properties by default. As a nice little bonus, you can try clicking one of the column headers - you will see that the DataGrid supports sorting right out of the box!

The last and empty row will let you add to the data source, simply by filling out the cells.

+++

## Summary

As you can see, it's extremely easy to get started with the DataGrid, but it's also a highly customizable control. In the next chapters, we'll look into all the cool stuff you can do with the DataGrid, so read on.

---

# DataGrid columns

In the previous chapter, we had a look at just how easy you could get a WPF DataGrid up and running. One of the reasons why it was so easy is the fact that the DataGrid will automatically generate appropriate columns for you, based on the data source you use.

+++

However, in some situations you might want to manually define the columns shown, either because you don’t want all the properties/columns of the data source, or because you want to be in control of which inline editors are used.

+++

## Manually defined columns

Let's try an example that looks a lot like the one in the previous chapter, but where we define all the columns manually, for maximum control. You can select the column type based on the data that you wish to display/edit. 

+++

As of writing, the following column types are available:

*   DataGridTextColumn
*   DataGridCheckBoxColumn
*   DataGridComboBoxColumn
*   DataGridHyperlinkColumn
*   DataGridTemplateColumn


+++


Especially the last one, the DataGridTemplateColumn, is interesting. It allows you to define any kind of content, which opens up the opportunity to use custom controls, either from the WPF library or even your own or 3rd party controls. Here's an example:

+++

```XML
<Window x:Class="WpfTutorialSamples.DataGrid_control.DataGridColumnsSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DataGridColumnsSample" Height="200" Width="300">
    <Grid Margin="10">
                <DataGrid Name="dgUsers" AutoGenerateColumns="False">
                        <DataGrid.Columns>

                                <DataGridTextColumn Header="Name" Binding="{Binding Name}" />

                                <DataGridTemplateColumn Header="Birthday">
                                        <DataGridTemplateColumn.CellTemplate>
                                                <DataTemplate>
                                                        <DatePicker SelectedDate="{Binding Birthday}" BorderThickness="0" />
                                                </DataTemplate>
                                        </DataGridTemplateColumn.CellTemplate>
                                </DataGridTemplateColumn>

                        </DataGrid.Columns>
                </DataGrid>
        </Grid>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfTutorialSamples.DataGrid_control
{
        public partial class DataGridColumnsSample : Window
        {
                public DataGridColumnsSample()
                {
                        InitializeComponent();

                        List<User> users = new List<User>();
                        users.Add(new User() { Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23) });
                        users.Add(new User() { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17) });
                        users.Add(new User() { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2) });

                        dgUsers.ItemsSource = users;
                }
        }

        public class User
        {
                public int Id { get; set; }

                public string Name { get; set; }

                public DateTime Birthday { get; set; }
        }
}
```

+++

![A DataGrid with custom columns](http://www.wpf-tutorial.com/chapters/datagrid/images/datagrid_columns.png "A DataGrid with custom columns")

+++

In the markup, I have added the AutoGenerateColumns property on the DataGrid, which I have set to false, to get control of the columns used. As you can see, I have left out the ID column, as I decided that I didn't care for it for this example. 

+++

For the Name property, I've used a simple text based column, so the most interesting part of this example comes with the Birthday column, where I've used a DataGridTemplateColumn with a DatePicker control inside of it. This allows the end-user to pick the date from a calendar, instead of having to manually enter it, as you can see on the screenshot.

+++

## Summary

By turning off automatically generated columns using the AutoGenerateColumns property, you get full control of which columns are shown and how their data should be viewed and edited. As seen by the example of this article, this opens up for some pretty interesting possibilities, where you can completely customize the editor and thereby enhance the end-user experience.

---

# DataGrid with row details

A very common usage scenario when using a DataGrid control is the ability to show details about each row, typically right below the row itself. The WPF DataGrid control supports this very well, and fortunately it's also very easy to use. Let's start off with an example and then we'll discuss how it works and the options it gives you afterwards:

+++

```XML
<Window x:Class="WpfTutorialSamples.DataGrid_control.DataGridDetailsSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DataGridDetailsSample" Height="200" Width="400">
        <Grid Margin="10">
                <DataGrid Name="dgUsers" AutoGenerateColumns="False">
                        <DataGrid.Columns>
                                <DataGridTextColumn Header="Name" Binding="{Binding Name}" />
                                <DataGridTextColumn Header="Birthday" Binding="{Binding Birthday}" />
                        </DataGrid.Columns>
                        <DataGrid.RowDetailsTemplate>
                                <DataTemplate>
                                        <TextBlock Text="{Binding Details}" Margin="10" />
                                </DataTemplate>
                        </DataGrid.RowDetailsTemplate>
                </DataGrid>
        </Grid>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfTutorialSamples.DataGrid_control
{
        public partial class DataGridDetailsSample : Window
        {
                public DataGridDetailsSample()
                {
                        InitializeComponent();
                        List<User> users = new List<User>();
                        users.Add(new User() { Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23) });
                        users.Add(new User() { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17) });
                        users.Add(new User() { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2) });

                        dgUsers.ItemsSource = users;
                }
        }

        public class User
        {
                public int Id { get; set; }

                public string Name { get; set; }

                public DateTime Birthday { get; set; }

                public string Details
                {
                        get
                        {
                                return String.Format("{0} was born on {1} and this is a long description of the person.", this.Name, this.Birthday.ToLongDateString());
                        }
                }
        }
}
```

+++

![A DataGrid with a simple details row](http://www.wpf-tutorial.com/chapters/datagrid/images/datagrid_details.png "A DataGrid with a simple details row")

+++

As you can see, I have expanded the example from previous chapters with a new property on the User class: The Description property. It simply returns a bit of information about the user in question, for our details row.

+++

In the markup, I have defined a couple of columns and then I use the **RowDetailsTemplate** to specify a template for the row details. As you can see, it works much like any other WPF template, where I use a DataTemplate with one or several controls inside of it, along with a standard binding against a property on the data source, in this case the Description property.

+++

As you can see from the resulting screenshot, or if you run the sample yourself, the details are now shown below the selected row. As soon as you select another row, the details for that row will be shown and the details for the previously selected row will be hidden.

+++

## Controlling row details visibility

Using the **RowDetailsVisibilityMode** property, you can change the above mentioned behavior though. It defaults to**VisibleWhenSelected**, where details are only visible when its parent row is selected, but you can change it to **Visible** or **Collapsed**. If you set it to Visible, all details rows will be visible all the time, like this:

+++

![A DataGrid with a all details rows visible at the same time](http://www.wpf-tutorial.com/chapters/datagrid/images/datagrid_details_visible.png "A DataGrid with a all details rows visible at the same time")

If you set it to Collapsed, all details will be invisible all the time.

+++

## More details

The first example of this article might have been a tad boring, using just a single, plain TextBlock control. Of course, with this being a DataTemplate, you can do pretty much whatever you want, so I decided to extend the example a bit, to give a better idea of the possibilities. Here's how it looks now:

![A DataGrid with a more complex details row](http://www.wpf-tutorial.com/chapters/datagrid/images/datagrid_details_more.png "A DataGrid with a more complex details row")

+++

As you can see from the code listing, it's mostly about expanding the details template into using a panel, which in turn can host more panels and/or controls. Using a Grid panel, we can get the tabular look of the user data, and an Image control allows us to show a picture of the user.

+++

```XML
<Window x:Class="WpfTutorialSamples.DataGrid_control.DataGridDetailsSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DataGridDetailsSample" Height="300" Width="300">
        <Grid Margin="10">
                <DataGrid Name="dgUsers" AutoGenerateColumns="False">
                        <DataGrid.Columns>
                                <DataGridTextColumn Header="Name" Binding="{Binding Name}" />
                                <DataGridTextColumn Header="Birthday" Binding="{Binding Birthday}" />
                        </DataGrid.Columns>
                        <DataGrid.RowDetailsTemplate>
                                <DataTemplate>
                                        <DockPanel Background="GhostWhite">
                                                <Image DockPanel.Dock="Left" Source="{Binding ImageUrl}" Height="64" Margin="10" />
                                                <Grid Margin="0,10">
                                                        <Grid.ColumnDefinitions>
                                                                <ColumnDefinition Width="Auto" />
                                                                <ColumnDefinition Width="*" />
                                                        </Grid.ColumnDefinitions>
                                                        <Grid.RowDefinitions>
                                                                <RowDefinition Height="Auto" />
                                                                <RowDefinition Height="Auto" />
                                                                <RowDefinition Height="Auto" />
                                                        </Grid.RowDefinitions>

                                                        <TextBlock Text="ID: " FontWeight="Bold" />
                                                        <TextBlock Text="{Binding Id}" Grid.Column="1" />
                                                        <TextBlock Text="Name: " FontWeight="Bold" Grid.Row="1" />
                                                        <TextBlock Text="{Binding Name}" Grid.Column="1" Grid.Row="1" />
                                                        <TextBlock Text="Birthday: " FontWeight="Bold" Grid.Row="2" />
                                                        <TextBlock Text="{Binding Birthday, StringFormat=d}" Grid.Column="1" Grid.Row="2" />

                                                </Grid>
                                        </DockPanel>
                                </DataTemplate>
                        </DataGrid.RowDetailsTemplate>
                </DataGrid>
        </Grid>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfTutorialSamples.DataGrid_control
{
        public partial class DataGridDetailsSample : Window
        {
                public DataGridDetailsSample()
                {
                        InitializeComponent();
                        List<User> users = new List<User>();
                        users.Add(new User() { Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23), ImageUrl = "http://www.wpf-tutorial.com/images/misc/john_doe.jpg" });
                        users.Add(new User() { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17) });
                        users.Add(new User() { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2) });

                        dgUsers.ItemsSource = users;
                }
        }

        public class User
        {
                public int Id { get; set; }

                public string Name { get; set; }

                public DateTime Birthday { get; set; }

                public string ImageUrl { get; set; }
        }
}
```

+++

## Summary

Being able to show details for a DataGrid row is extremely useful, and with the WPF DataGrid it's both easy and highly customizable, as you can see from the examples provided in this tutorial.

