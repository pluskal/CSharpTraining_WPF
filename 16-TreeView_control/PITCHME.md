# TreeView introduction

The TreeView control enabled you to display hierarchical data, with each piece of data represented by a node in the tree. Each node can then have child nodes, and the child nodes can have child nodes and so on. If you have ever used the Windows Explorer, you also know how a TreeView looks - it's the control that shows the current folder structure on your machine, in the left part of the Windows Explorer window.

+++

## TreeView in WPF vs. WinForms

If you have previously worked with the TreeView control in WinForms, you might think of the TreeView control as one that's easy to use but hard to customize. In WPF it's a little bit the other way around, at least for newbies: It feels a bit complicated to get started with, but it's a LOT easier to customize. Just like most other WPF controls, the TreeView is almost lookless once you start, but it can be styled almost endlessly without much effort.

+++

Just like with the ListView control, the TreeView control does have its own item type, the TreeViewItem, which you can use to populate the TreeView. If you come from the WinForms world, you will likely start by generating TreeViewItem's and adding them to the Items property, and this is indeed possible. But since this is WPF, the preferred way is to bind the TreeView to a hierarchical data structure and then use an appropriate template to render the content.

+++

We'll show you how to do it both ways, and while the good, old WinForms inspired way might seem like the easy choice at first, you should definitely give the WPF way a try - in the long run, it offers more flexibility and will fit in better with the rest of the WPF code you write.

+++

## Summary

The WPF TreeView is indeed a complex control. In the first example, which we'll get into already in the next chapter, it might seem simple, but once you dig deeper, you'll see the complexity. Fortunately, the WPF TreeView control rewards you with great usability and flexibility. To show you all of them, we have dedicated an entire category to all the TreeView articles. Click on to the next one to get started.

---

# A simple TreeView example

As we talked about in the previous article, the WPF TreeView can be used in a very simple manner, by adding TreeViewItem objects to it, either from Code-behind or simply by declaring them directly in your XAML. This is indeed very easy to get started with, as you can see from the example here:

+++

```XML
<Window x:Class="WpfTutorialSamples.TreeView_control.TreeViewSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TreeViewSample" Height="200" Width="250">
    <Grid Margin="10">
                <TreeView>
                        <TreeViewItem Header="Level 1" IsExpanded="True">
                                <TreeViewItem Header="Level 2.1" />
                                <TreeViewItem Header="Level 2.2" IsExpanded="True">
                                        <TreeViewItem Header="Level 3.1" />
                                        <TreeViewItem Header="Level 3.2" />
                                </TreeViewItem>
                                <TreeViewItem Header="Level 2.3" />
                        </TreeViewItem>
                </TreeView>
        </Grid>
</Window>
```

+++

![A simple TreeView control with items defined in the markup](http://www.wpf-tutorial.com/chapters/treeview/images/treeview_simple.png "A simple TreeView control with items defined in the markup")

+++

We simply declare the TreeViewItem objects directly in the XAML, in the same structure that we want to display them in, where the first tag is a child of the TreeView control and its child objects are also child tags to its parent object. To specify the text we want displayed for each node, we use the**Header** property. By default, a TreeViewItem is not expanded, but to show you the structure of the example, I have used the **IsExpanded** property to expand the two parent items.

+++

## TreeViewItem's with images and other controls

The **Header** is an interesting property, though. As you can see, I can just specify a text string and then have it rendered directly without doing anything else, but this is WPF being nice to us - internally, it wraps the text inside of a TextBlock control, instead of forcing you to do it. This shows us that we can stuff pretty much whatever we want to into the Header property instead of just a string and then have the TreeView render it - a great example of why it's so easy to customize the look of WPF controls.

+++

One of the common requests from people coming from WinForms or even other UI libraries is the ability to show an image next to the text label of a TreeView item. This is very easy to do with WinForms, because the TreeView is built exactly for this scenario. With the WPF TreeView, it's a bit more complex, but you're rewarded with a lot more flexibility than you could ever get from the WinForms TreeView. Here's an example of it:

+++

```XML
<Window x:Class="WpfTutorialSamples.TreeView_control.TreeViewCustomItemsSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TreeViewCustomItemsSample" Height="200" Width="250">
        <Grid Margin="10">
                <TreeView>
                        <TreeViewItem IsExpanded="True">
                                <TreeViewItem.Header>
                                        <StackPanel Orientation="Horizontal">
                                                <Image Source="/WpfTutorialSamples;component/Images/bullet_blue.png" />
                                                <TextBlock Text="Level 1 (Blue)" />
                                        </StackPanel>
                                </TreeViewItem.Header>
                                <TreeViewItem>
                                        <TreeViewItem.Header>
                                                <StackPanel Orientation="Horizontal">
                                                        <TextBlock Text="Level 2.1" Foreground="Blue" />
                                                </StackPanel>
                                        </TreeViewItem.Header>
                                </TreeViewItem>
                                <TreeViewItem IsExpanded="True">
                                        <TreeViewItem.Header>
                                                <StackPanel Orientation="Horizontal">
                                                        <Image Source="/WpfTutorialSamples;component/Images/bullet_green.png" />
                                                        <TextBlock Text="Level 2.2 (Green)" Foreground="Blue" />
                                                </StackPanel>
                                        </TreeViewItem.Header>
                                        <TreeViewItem>
                                                <TreeViewItem.Header>
                                                        <TextBlock Text="Level 3.1" Foreground="Green" />
                                                </TreeViewItem.Header>
                                        </TreeViewItem>
                                        <TreeViewItem>
                                                <TreeViewItem.Header>
                                                        <TextBlock Text="Level 3.2" Foreground="Green" />
                                                </TreeViewItem.Header>
                                        </TreeViewItem>
                                </TreeViewItem>
                                <TreeViewItem>
                                        <TreeViewItem.Header>
                                                <TextBlock Text="Level 2.3" Foreground="Blue" />
                                        </TreeViewItem.Header>
                                </TreeViewItem>
                        </TreeViewItem>
                </TreeView>
        </Grid>
</Window>
```

+++

![A TreeView control with items defined in the markup, using the Header property for custom content like images](http://www.wpf-tutorial.com/chapters/treeview/images/treeview_custom.png "A TreeView control with items defined in the markup, using the Header property for custom content like images")

+++

I did a whole bunch of things here, just to show you the kind of flexibility you get: I colored the child items and I added images and even buttons to the parent items. Because we're defining the entire thing with simple markup, you can do almost anything, but as you can see from the example code, it does come with a price: Huge amounts of XAML code, for a tree with just six nodes in total!

+++

## Summary

While it is entirely possible to define an entire TreeView just using markup, as we did in the above examples, it's not the best approach in most situations, and while you could do it from Code-behind instead, this would have resulted in even more lines of code. Once again the solution is **data binding**, which we'll look into in the next chapters.

---

# TreeView, data binding and multiple templates

The WPF TreeView supports data binding, like pretty much all other WPF controls does, but because the TreeView is hierarchical in nature, a normal DataTemplate often won't suffice. 

+++

Instead, we use the HierarchicalDataTemplate, which allows us to template both the tree node itself, while controlling which property to use as a source for child items of the node.

+++

## A basic data bound TreeView

In the following example, I'll show you just how easy it is to get started with the HierarchicalDataTemplate:

+++

```XML
<Window x:Class="WpfTutorialSamples.TreeView_control.TreeViewDataBindingSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:self="clr-namespace:WpfTutorialSamples.TreeView_control"
        Title="TreeViewDataBindingSample" Height="150" Width="200">
    <Grid Margin="10">
                <TreeView Name="trvMenu">
                        <TreeView.ItemTemplate>
                                <HierarchicalDataTemplate DataType="{x:Type self:MenuItem}" ItemsSource="{Binding Items}">
                                        <TextBlock Text="{Binding Title}" />
                                </HierarchicalDataTemplate>
                        </TreeView.ItemTemplate>
                </TreeView>
        </Grid>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Collections.ObjectModel;

namespace WpfTutorialSamples.TreeView_control
{
        public partial class TreeViewDataBindingSample : Window
        {
                public TreeViewDataBindingSample()
                {
                        InitializeComponent();
                        MenuItem root = new MenuItem() { Title = "Menu" };
                        MenuItem childItem1 = new MenuItem() { Title = "Child item #1" };
                        childItem1.Items.Add(new MenuItem() { Title = "Child item #1.1" });
                        childItem1.Items.Add(new MenuItem() { Title = "Child item #1.2" });
                        root.Items.Add(childItem1);
                        root.Items.Add(new MenuItem() { Title = "Child item #2" });
                        trvMenu.Items.Add(root);
                }
        }

        public class MenuItem
        {
                public MenuItem()
                {
                        this.Items = new ObservableCollection<MenuItem>();
                }

                public string Title { get; set; }

                public ObservableCollection<MenuItem> Items { get; set; }
        }

}
```

+++

![A simple TreeView control, using data binding](http://www.wpf-tutorial.com/chapters/treeview/images/treeview_databinding.png "A simple TreeView control, using data binding")

+++

In the XAML markup, I have specified a HierarchicalDataTemplate for the **ItemTemplate** of the TreeView. I instruct it to use the **Items** property for finding child items, by setting the **ItemsSource** property of the template, and inside of it I define the actual template, which for now just consists of a TextBlock bound to the **Title** property.

+++

This first example was very simple, in fact so simple that we might as well have just added the TreeView items manually, instead of generating a set of objects and then binding to them. However, as soon as things get a bit more complicated, the advantages of using data bindings gets more obvious.

+++

## Multiple templates for different types

In the next example, I've taken a slightly more complex case, where I want to show a tree of families and their members. A family should be represented in one way, while each of its members should be shown in another way. I achieve this by creating two different templates and specifying them as resources of the tree (or the Window or the Application - that's really up to you), and then allowing the TreeView to pick the correct template based on the underlying type of data.

+++

```XML
<Window x:Class="WpfTutorialSamples.TreeView_control.TreeViewMultipleTemplatesSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:self="clr-namespace:WpfTutorialSamples.TreeView_control"
        Title="TreeViewMultipleTemplatesSample" Height="200" Width="250">
        <Grid Margin="10">
                <TreeView Name="trvFamilies">
                        <TreeView.Resources>
                                <HierarchicalDataTemplate DataType="{x:Type self:Family}" ItemsSource="{Binding Members}">
                                        <StackPanel Orientation="Horizontal">
                                                <Image Source="/WpfTutorialSamples;component/Images/group.png" Margin="0,0,5,0" />
                                                <TextBlock Text="{Binding Name}" />
                                                <TextBlock Text=" [" Foreground="Blue" />
                                                <TextBlock Text="{Binding Members.Count}" Foreground="Blue" />
                                                <TextBlock Text="]" Foreground="Blue" />
                                        </StackPanel>
                                </HierarchicalDataTemplate>
                                <DataTemplate DataType="{x:Type self:FamilyMember}">
                                        <StackPanel Orientation="Horizontal">
                                                <Image Source="/WpfTutorialSamples;component/Images/user.png" Margin="0,0,5,0" />
                                                <TextBlock Text="{Binding Name}" />
                                                <TextBlock Text=" (" Foreground="Green" />
                                                <TextBlock Text="{Binding Age}" Foreground="Green" />
                                                <TextBlock Text=" years)" Foreground="Green" />
                                        </StackPanel>
                                </DataTemplate>
                        </TreeView.Resources>
                </TreeView>
        </Grid>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;

namespace WpfTutorialSamples.TreeView_control
{
        public partial class TreeViewMultipleTemplatesSample : Window
        {
                public TreeViewMultipleTemplatesSample()
                {
                        InitializeComponent();

                        List<Family> families = new List<Family>();

                        Family family1 = new Family() { Name = "The Doe's" };
                        family1.Members.Add(new FamilyMember() { Name = "John Doe", Age = 42 });
                        family1.Members.Add(new FamilyMember() { Name = "Jane Doe", Age = 39 });
                        family1.Members.Add(new FamilyMember() { Name = "Sammy Doe", Age = 13 });
                        families.Add(family1);

                        Family family2 = new Family() { Name = "The Moe's" };
                        family2.Members.Add(new FamilyMember() { Name = "Mark Moe", Age = 31 });
                        family2.Members.Add(new FamilyMember() { Name = "Norma Moe", Age = 28 });
                        families.Add(family2);

                        trvFamilies.ItemsSource = families;
                }
        }

        public class Family
        {
                public Family()
                {
                        this.Members = new ObservableCollection<FamilyMember>();
                }

                public string Name { get; set; }

                public ObservableCollection<FamilyMember> Members { get; set; }
        }

        public class FamilyMember
        {
                public string Name { get; set; }

                public int Age { get; set; }
        }
}
```

+++

![A more complex TreeView control, using data binding with several templates](http://www.wpf-tutorial.com/chapters/treeview/images/treeview_multiple_templates.png "A more complex TreeView control, using data binding with several templates")

+++

As mentioned, the two templates are declared as a part of the TreeView resources, allowing the TreeView to select the appropriate template based on the data type that it's about to show. The template defined for the **Family** type is a hierarchical template, using the **Members** property to show its family members.

+++

The template defined for the **FamilyMember** type is a regular DataTemplate, since this type doesn't have any child members. However, if we had wanted each FamilyMember to keep a collection of their children and perhaps their children's children, then we would have used a hierarchical template instead.

+++

In both templates, we use an image representing either a family or a family member, and then we show some interesting data about it as well, like the amount of family members or the person's age.

+++

In the code-behind, we simply create two Family instances, fill each of them with a set of members, and then add each of the families to a list, which is then used as the items source for the TreeView.

+++

## Summary

Using data binding, the TreeView is very customizable and with the ability to specify multiple templates for rendering different data types, the possibilities are almost endless.

---

# TreeView - Selection/Expansion state

In the previous couple of TreeView articles, we used data binding to display custom objects in a WPF TreeView. 

+++

This works really well, but it does leave you with one problem: Because each tree node is now represented by your custom class, for instance FamilyMember as we saw in the previous article, you no longer have direct control over TreeView node specific functionality like selection and expansion state. In praxis this means that you can't select or expand/collapse a given node from code-behind.

+++

Lots of solutions exists to handle this, ranging from "hacks" where you use the item generators of the TreeView to get the underlying TreeViewItem, where you can control the IsExpanded and IsSelected properties, to much more advanced MVVM-inspired implementations. In this article I would like to show you a solution that lies somewhere in the middle, making it easy to implement and use, while still not being a complete hack.

+++

## A TreeView selection/expansion solution

The basic principle is to implement two extra properties on your data class: IsExpanded and IsSelected. These two properties are then hooked up to the TreeView, using a couple of styles targeting the TreeViewItem, inside of the **ItemContainerStyle** for the TreeView.

+++

You could easily implement these two properties on all of your objects, but it's much easier to inherit them from a base object. If this is not feasible for your solution, you could create an interface for it and then implement this instead, to establish a common ground. For this example, I've chosen the base class method, because it allows me to very easily get the same functionality for my other objects. Here's the code:

+++

```XML
<Window x:Class="WpfTutorialSamples.TreeView_control.TreeViewSelectionExpansionSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TreeViewSelectionExpansionSample" Height="200" Width="300">
        <DockPanel Margin="10">
                <WrapPanel Margin="0,10,0,0" DockPanel.Dock="Bottom" HorizontalAlignment="Center">
                        <Button Name="btnSelectNext" Click="btnSelectNext_Click" Width="120">Select next</Button>
                        <Button Name="btnToggleExpansion" Click="btnToggleExpansion_Click" Width="120" Margin="10,0,0,0">Toggle expansion</Button>
                </WrapPanel>

                <TreeView Name="trvPersons">
                        <TreeView.ItemTemplate>
                                <HierarchicalDataTemplate ItemsSource="{Binding Children}">
                                        <StackPanel Orientation="Horizontal">
                                                <Image Source="/WpfTutorialSamples;component/Images/user.png" Margin="0,0,5,0" />
                                                <TextBlock Text="{Binding Name}" Margin="0,0,4,0" />
                                        </StackPanel>
                                </HierarchicalDataTemplate>
                        </TreeView.ItemTemplate>
                        <TreeView.ItemContainerStyle>
                                <Style TargetType="TreeViewItem">
                                        <Setter Property="IsSelected" Value="{Binding IsSelected}" />
                                        <Setter Property="IsExpanded" Value="{Binding IsExpanded}" />
                                </Style>
                        </TreeView.ItemContainerStyle>
                </TreeView>
        </DockPanel>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace WpfTutorialSamples.TreeView_control
{
        public partial class TreeViewSelectionExpansionSample : Window
        {
                public TreeViewSelectionExpansionSample()
                {
                        InitializeComponent();

                        List<Person> persons = new List<Person>();
                        Person person1 = new Person() { Name = "John Doe", Age = 42 };

                        Person person2 = new Person() { Name = "Jane Doe", Age = 39 };

                        Person child1 = new Person() { Name = "Sammy Doe", Age = 13 };
                        person1.Children.Add(child1);
                        person2.Children.Add(child1);

                        person2.Children.Add(new Person() { Name = "Jenny Moe", Age = 17 });

                        Person person3 = new Person() { Name = "Becky Toe", Age = 25 };

                        persons.Add(person1);
                        persons.Add(person2);
                        persons.Add(person3);

                        person2.IsExpanded = true;
                        person2.IsSelected = true;

                        trvPersons.ItemsSource = persons;
                }

                private void btnSelectNext_Click(object sender, RoutedEventArgs e)
                {
                        if(trvPersons.SelectedItem != null)
                        {
                                var list = (trvPersons.ItemsSource as List<Person>);
                                int curIndex = list.IndexOf(trvPersons.SelectedItem as Person);
                                if(curIndex >= 0)
                                        curIndex++;
                                if(curIndex >= list.Count)
                                        curIndex = 0;
                                if(curIndex >= 0)
                                        list[curIndex].IsSelected = true;
                        }
                }

                private void btnToggleExpansion_Click(object sender, RoutedEventArgs e)
                {
                        if(trvPersons.SelectedItem != null)
                                (trvPersons.SelectedItem as Person).IsExpanded = !(trvPersons.SelectedItem as Person).IsExpanded;
                }



        }

        public class Person : TreeViewItemBase
        {
                public Person()
                {
                        this.Children = new ObservableCollection<Person>();
                }

                public string Name { get; set; }

                public int Age { get; set; }

                public ObservableCollection<Person> Children { get; set; }
        }

        public class TreeViewItemBase : INotifyPropertyChanged
        {
                private bool isSelected;
                public bool IsSelected
                {
                        get { return this.isSelected; }
                        set
                        {
                                if(value != this.isSelected)
                                {
                                        this.isSelected = value;
                                        NotifyPropertyChanged("IsSelected");
                                }
                        }
                }

                private bool isExpanded;
                public bool IsExpanded
                {
                        get { return this.isExpanded; }
                        set
                        {
                                if(value != this.isExpanded)
                                {
                                        this.isExpanded = value;
                                        NotifyPropertyChanged("IsExpanded");
                                }
                        }
                }


                public event PropertyChangedEventHandler PropertyChanged;

                public void NotifyPropertyChanged(string propName)
                {
                        if(this.PropertyChanged != null)
                                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }
        }
}
```

![A TreeView control with a custom template, handling IsSelected and IsExpanded](http://www.wpf-tutorial.com/chapters/treeview/images/treeview_selection_expansion.png "A TreeView control with a custom template, handling IsSelected and IsExpanded")

+++

I'm sorry for the rather large amount of code in one place. In a real world solution, it would obviously be spread out over multiple files instead and the data for the tree would likely come from an actual data source, instead of being generated on the fly. Allow me to explain what happens in the example.

+++

## XAML part

I have defined a couple of buttons to be placed in the bottom of the dialog, to use the two new properties. Then we have the TreeView, for which I have defined an ItemTemplate (as demonstrated in a previous chapter) as well as an ItemContainerStyle. 

+++

If you haven't read the chapters on styling yet, you might not completely understand that part, but it's simply a matter of tying together the properties on our own custom class with the **IsSelected** and **IsExpanded** properties on the TreeViewItems, which is done with Style setters. You can learn more about them elsewhere in this tutorial.

+++

## Code-behind part

In the code-behind, I have defined a **Person** class, with a couple of properties, which inherits our extra properties from the **TreeViewItemBase** class. You should be aware that the TreeViewItemBase class implements the INotifyPropertyChanged interface and uses it to notify of changes to these two essential properties - without this, selection/expansion changes won't be reflected in the UI. The concept of notification changes are explained in the Data binding chapters.

+++

In the main Window class I simply create a range of persons, while adding children to some of them. I add the persons to a list, which I assign as the ItemsSource of the TreeView, which, with a bit of help from the defined template, renders them the way they are shown on the screenshot.

+++

The most interesting part happens when I set the IsExpanded and IsSelected properties on the _person2_ object. This is what causes the second person (Jane Doe) to be initially selected and expanded, as shown on the screenshot. We also use these two properties on the Person objects (inherited from the TreeViewItemBase class) in the event handlers for the two test buttons (please bear in mind that, to keep the code as small and simple as possible, the selection button only works for the top level items).

+++

## Summary

By creating and implementing a base class for the objects that you wish to use and manipulate within a TreeView, and using the gained properties in the ItemContainerStyle, you make it a lot easier to work with selections and expansion states. There are many solutions to tackle this problem with, and while this should do the trick, you might be able to find a solution that fits your needs better. As always with programming, it's all about using the right tool for the job at hand.

---

# Lazy loading TreeView items

The usual process when using the TreeView is to bind to a collection of items or to manually add each level at the same time. However, in some situations, you want to delay the loading of a nodes child items until they are actually needed. 

+++

This is especially useful if you have a very deep tree, with lots of levels and child nodes and a great example of this, is the folder structure of your Windows computer.

+++

Each drive on your Windows computer has a range of child folders, and each of those child folders have child folders beneath them and so on. Looping through each drive and each drives child folders could become extremely time consuming and your TreeView would soon consist of a lot of nodes, with a high percentage of them never being needed. This is the perfect task for a lazy-loaded TreeView, where child folders are only loaded on demand.

+++

To achieve this, we simply add a dummy folder to each drive or child folder, and then when the user expands it, we remove the dummy folder and replace it with the actual values. This is how our application looks when it starts - by that time, we have only obtained a list of available drives on the computer:

+++

![A TreeView showing the drive structure](http://www.wpf-tutorial.com/chapters/treeview/images/lazy_load_drives.png "A TreeView showing the drive structure")

+++

You can now start expanding the nodes, and the application will automatically load the sub folders. If a folder is empty, it will be shown as empty once you try to expand it, as it can be seen on the next screenshot:

+++

![A TreeView showing the drive and folder structure](http://www.wpf-tutorial.com/chapters/treeview/images/lazy_load_folders.png "A TreeView showing the drive and folder structure")

+++

So how is it accomplished? Let's have a look at the code:

```XML
<Window x:Class="WpfTutorialSamples.TreeView_control.LazyLoadingSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="LazyLoadingSample" Height="300" Width="300">
    <Grid>
        <TreeView Name="trvStructure" TreeViewItem.Expanded="TreeViewItem_Expanded" Margin="10" />
    </Grid>
</Window>
```

+++

```C#
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfTutorialSamples.TreeView_control
{
        public partial class LazyLoadingSample : Window
        {
                public LazyLoadingSample()
                {
                        InitializeComponent();
                        DriveInfo[] drives = DriveInfo.GetDrives();
                        foreach(DriveInfo driveInfo in drives)
                                trvStructure.Items.Add(CreateTreeItem(driveInfo));
                }

                public void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
                {
                        TreeViewItem item = e.Source as TreeViewItem;
                        if((item.Items.Count == 1) && (item.Items[0] is string))
                        {
                                item.Items.Clear();

                                DirectoryInfo expandedDir = null;
                                if(item.Tag is DriveInfo)
                                        expandedDir = (item.Tag as DriveInfo).RootDirectory;
                                if(item.Tag is DirectoryInfo)
                                        expandedDir = (item.Tag as DirectoryInfo);
                                try
                                {
                                        foreach(DirectoryInfo subDir in expandedDir.GetDirectories())
                                                item.Items.Add(CreateTreeItem(subDir));
                                }
                                catch { }
                        }
                }

                private TreeViewItem CreateTreeItem(object o)
                {
                        TreeViewItem item = new TreeViewItem();
                        item.Header = o.ToString();
                        item.Tag = o;
                        item.Items.Add("Loading...");
                        return item;
                }
        }
}
```

+++

The **XAML** is very simple and only one interesting detail is present: The way we subscribe to the Expanded event of TreeViewItem's. Notice that this is indeed the TreeViewItem and not the TreeView itself, but because the event bubbles up, we are able to just capture it in one place for the entire TreeView, instead of having to subscribe to it for each item we add to the tree. This event gets called each time an item is expanded, which we need to be aware of to load its child items on demand.

+++

In **Code-behind**, we start by adding each drive found on the computer to the TreeView control. We assign the **DriveInfo** instance to the Tag property, so that we can later retrieve it. Notice that we use a custom method to create the TreeViewItem, called **CreateTreeItem()**, since we can use the exact same method when we want to dynamically add a child folder later on. Notice in this method how we add a child item to the Items collection, in the form of a string with the text "Loading...".

+++

Next up is the TreeViewItem_Expanded event. As already mentioned, this event is raised each time a TreeView item is expanded, so the first thing we do is to check whether this item has already been loaded, by checking if the child items currently consists of only one item, which is a string - if so, we have found the "Loading..." child item, which means that we should now load the actual contents and replace the placeholder item with it.

+++

We now use the items Tag property to get a reference to the **DriveInfo** or **DirectoryInfo** instance that the current item represents, and then we get a list of child directories, which we add to the clicked item, once again using the **CreateTreeItem()** method. Notice that the loop where we add each child folder is in a try..catch block - this is important, because some paths might not be accessible, usually for security reasons. You could grab the exception and use it to reflect this in the interface in one way or another.

+++

## Summary

By subscribing to the Expanded event, we can easily create a lazy-loaded TreeView, which can be a much better solution than a statically created one in several situations.
