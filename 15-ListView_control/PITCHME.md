# Introduction to the ListView control

The ListView control is very commonly used in Windows applications, to represent lists of data.

+++

A great example of this is the file lists in Windows Explorer, where each file can be shown by its name and, if desired, with columns containing information about the size, last modification date and so on.

+++

## ListView in WPF vs. WinForms

If you have previously worked with WinForms, then you have a good idea about how practical the ListView is, but you should be aware that the ListView in WPF isn't used like the WinForms version. Once again the main difference is that while the WinForms ListView simply calls Windows API functions to render a common Windows ListView control, the WPF ListView is an independent control that doesn't rely on the Windows API.

+++

The WPF ListView does use a ListViewItem class for its most basic items, but if you compare it to the WinForms version, you might start looking for properties like ImageIndex, Group and SubItems, but they're not there. The WPF ListView handles stuff like item images, groups and their sub items in a completely different way.

+++

## Summary

The ListView is a complex control, with lots of possibilities and especially in the WPF version, you get to customize it almost endlessly if you want to. For that reason, we have dedicated an entire category to all the ListView articles here on the site. Click on to the next article to get started.

---

# A simple ListView example

The WPF ListView control is very bare minimum in its most simple form. In fact, it will look a whole lot like the WPF ListBox, until you start adding specialized views to it. That's not so strange, since a ListView inherits directly from the ListBox control. So, a default ListView is actually just a ListBox, with a different selection mode (more on that later).

+++

Let's try creating a ListView in its most simple form:

```XML
<Window x:Class="WpfTutorialSamples.ListView_control.ListViewBasicSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListViewBasicSample" Height="200" Width="200">
    <Grid>
                <ListView Margin="10">
                        <ListViewItem>A ListView</ListViewItem>
                        <ListViewItem IsSelected="True">with several</ListViewItem>
                        <ListViewItem>items</ListViewItem>
                </ListView>
        </Grid>
</Window>
```

+++

![A simple ListView control](http://www.wpf-tutorial.com/chapters/listview/images/listview_simple.png "A simple ListView control")

This is pretty much as simple as it gets, using manually specified ListViewItem to fill the list and with nothing but a text label representing each item - a bare minimum WPF ListView control.

+++

## ListViewItem with an image

Because of the look-less nature of WPF, specifying an image for a ListViewItem isn't just about assigning an image ID or key to a property. Instead, you take full control of it and specify the controls needed to render both image and text in the ListViewItem. Here's an example:

+++

```XML
<Window x:Class="WpfTutorialSamples.ListView_control.ListViewBasicSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListViewBasicSample" Height="200" Width="200">
    <Grid>
                <ListView Margin="10">
                        <ListViewItem>
                                <StackPanel Orientation="Horizontal">
                                        <Image Source="/WpfTutorialSamples;component/Images/bullet_green.png" Margin="0,0,5,0" />
                                        <TextBlock>Green</TextBlock>
                                </StackPanel>
                        </ListViewItem>
                        <ListViewItem>
                                <StackPanel Orientation="Horizontal">
                                        <Image Source="/WpfTutorialSamples;component/Images/bullet_blue.png" Margin="0,0,5,0" />
                                        <TextBlock>Blue</TextBlock>
                                </StackPanel>
                        </ListViewItem>
                        <ListViewItem IsSelected="True">
                                <StackPanel Orientation="Horizontal">
                                        <Image Source="/WpfTutorialSamples;component/Images/bullet_red.png" Margin="0,0,5,0" />
                                        <TextBlock>Red</TextBlock>
                                </StackPanel>
                        </ListViewItem>
                </ListView>
        </Grid>
</Window>
```

+++

![A simple ListView control with images assigned to each item](http://www.wpf-tutorial.com/chapters/listview/images/listview_simple_images.png "A simple ListView control with images assigned to each item")

What we do here is very simple. Because the ListViewItem derives from the ContentControl class, we can specify a WPF control as its content. In this case, we use a StackPanel, which has an Image and a TextBlock as its child controls.

+++

## Summary

As you can see, building a ListView manually in XAML is very simple, but in most cases, your ListView data will come from some sort of data source, which should be rendered in the ListView at runtime. We will look into doing just that in the next chapter.

---

# ListView, data binding and ItemTemplate

In the previous article, we manually populated a ListView control through XAML code, but in WPF, it's all about data binding. 

+++

The concept of data binding is explained in detail in another part of this tutorial, but generally speaking it's about separating data from layout. So, let's try binding some data to a ListView:

+++

```XML
<Window x:Class="WpfTutorialSamples.ListView_control.ListViewDataBindingSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListViewDataBindingSample" Height="300" Width="300">
    <Grid>
                <ListView Margin="10" Name="lvDataBinding"></ListView>
        </Grid>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfTutorialSamples.ListView_control
{
        public partial class ListViewDataBindingSample : Window
        {
                public ListViewDataBindingSample()
                {
                        InitializeComponent();
                        List<User> items = new List<User>();
                        items.Add(new User() { Name = "John Doe", Age = 42 });
                        items.Add(new User() { Name = "Jane Doe", Age = 39 });
                        items.Add(new User() { Name = "Sammy Doe", Age = 13 });
                        lvDataBinding.ItemsSource = items;
                }
        }

        public class User
        {
                public string Name { get; set; }

                public int Age { get; set; }
        }
}
```

+++

We populate a list of our own User objects, each user having a name and an age. The data binding process happens automatically as soon as we assign the list to the ItemsSource property of the ListView, but the result is a bit discouraging:

+++

![A simple ListView control, using data binding](http://www.wpf-tutorial.com/chapters/listview/images/listview_databinding_simple.png "A simple ListView control, using data binding")

+++

Each user is represented by their type name in the ListView. This is to be expected, because .NET doesn't have a clue about how you want your data to be displayed, so it just calls the ToString() method on each object and uses that to represent the item.

+++

We can use that to our advantage and override the ToString() method, to get a more meaningful output. Try replacing the User class with this version:

```XML
public class User
{
        public string Name { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
                return this.Name + ", " + this.Age + " years old";
        }
}
```

+++

![A simple ListView control, using data binding and a ToString method on the source object](http://www.wpf-tutorial.com/chapters/listview/images/listview_databinding_tostring.png "A simple ListView control, using data binding and a ToString method on the source object")

+++

This is a much more user friendly display and will do just fine in some cases, but relying on a simple string is not that flexible. Perhaps you want a part of the text to be bold or another color? Perhaps you want an image? Fortunately, WPF makes all of this very simple using templates.

+++

## ListView with an ItemTemplate

WPF is all about templating, so specifying a data template for the ListView is very easy. In this example, we'll do a bunch of custom formatting in each item, just to show you how flexible this makes the WPF ListView.

+++

```XML
<Window x:Class="WpfTutorialSamples.ListView_control.ListViewItemTemplateSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListViewItemTemplateSample" Height="150" Width="350">
    <Grid>
                <ListView Margin="10" Name="lvDataBinding">
                        <ListView.ItemTemplate>
                                <DataTemplate>
                                        <WrapPanel>
                                                <TextBlock Text="Name: " />
                                                <TextBlock Text="{Binding Name}" FontWeight="Bold" />
                                                <TextBlock Text=", " />
                                                <TextBlock Text="Age: " />
                                                <TextBlock Text="{Binding Age}" FontWeight="Bold" />
                                                <TextBlock Text=" (" />
                                                <TextBlock Text="{Binding Mail}" TextDecorations="Underline" Foreground="Blue" Cursor="Hand" />
                                                <TextBlock Text=")" />
                                        </WrapPanel>
                                </DataTemplate>
                        </ListView.ItemTemplate>
                </ListView>
        </Grid>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfTutorialSamples.ListView_control
{
        public partial class ListViewItemTemplateSample : Window
        {
                public ListViewItemTemplateSample()
                {
                        InitializeComponent();
                        List<User> items = new List<User>();
                        items.Add(new User() { Name = "John Doe", Age = 42, Mail = "john@doe-family.com" });
                        items.Add(new User() { Name = "Jane Doe", Age = 39, Mail = "jane@doe-family.com" });
                        items.Add(new User() { Name = "Sammy Doe", Age = 13, Mail = "sammy.doe@gmail.com" });
                        lvDataBinding.ItemsSource = items;
                }
        }

        public class User
        {
                public string Name { get; set; }

                public int Age { get; set; }

                public string Mail { get; set; }
        }
}
```

+++

![A ListView control, using data binding with an ItemTemplate](http://www.wpf-tutorial.com/chapters/listview/images/listview_databinding_itemtemplate.png "A ListView control, using data binding with an ItemTemplate")

+++

We use a bunch of TextBlock controls to build each item, where we put part of the text in bold. For the e-mail address, which we added to this example, we underline it, give it a blue color and change the mouse cursor, to make it behave like a hyperlink.

+++

## Summary

Using an ItemTemplate and data binding, we produced a pretty cool ListView control. However, it still looks a lot like a ListBox. A very common usage scenario for a ListView is to have columns, sometimes (e.g. in WinForms) referred to as a details view. WPF comes with a built-in view class to handle this, which we will talk about in the next chapter.

---

# ListView with a GridView

In the previous ListView articles, we have used the most basic version of the WPF ListView, which is the one without a custom View specified. This results in a ListView that acts very much like the WPF ListBox, with some subtle differences. The real power lies in the views though and WPF comes with one specialized view built-in: The GridView.

+++

By using the GridView, you can get several columns of data in your ListView, much like you see it in Windows Explorer. Just to make sure that everyone can visualize it, we'll start off with a basic example:

+++

```XML
<Window x:Class="WpfTutorialSamples.ListView_control.ListViewGridViewSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListViewGridViewSample" Height="200" Width="400">
    <Grid>
                <ListView Margin="10" Name="lvUsers">
                        <ListView.View>
                                <GridView>
                                        <GridViewColumn Header="Name" Width="120" DisplayMemberBinding="{Binding Name}" />
                                        <GridViewColumn Header="Age" Width="50" DisplayMemberBinding="{Binding Age}" />
                                        <GridViewColumn Header="Mail" Width="150" DisplayMemberBinding="{Binding Mail}" />
                                </GridView>
                        </ListView.View>
                </ListView>
        </Grid>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfTutorialSamples.ListView_control
{
        public partial class ListViewGridViewSample : Window
        {
                public ListViewGridViewSample()
                {
                        InitializeComponent();
                        List<User> items = new List<User>();
                        items.Add(new User() { Name = "John Doe", Age = 42, Mail = "john@doe-family.com" });
                        items.Add(new User() { Name = "Jane Doe", Age = 39, Mail = "jane@doe-family.com" });
                        items.Add(new User() { Name = "Sammy Doe", Age = 7, Mail = "sammy.doe@gmail.com" });
                        lvUsers.ItemsSource = items;
                }
        }

        public class User
        {
                public string Name { get; set; }

                public int Age { get; set; }

                public string Mail { get; set; }
        }
}
```

+++

![A ListView using a GridView for layout](http://www.wpf-tutorial.com/chapters/listview/images/listview_gridview_simple.png "A ListView using a GridView for layout")

+++

So, we use the same User class as previously, for test data, which we then bind to the ListView. This is all the same as we saw in previous chapters, but as you can see from the screenshot, the layout is very different. This is the power of data binding - the same data, but presented in a completely different way, just by changing the markup.

+++

In the markup (XAML), we define a View for the ListView, using the ListView.View property. We set it to a GridView, which is currently the only included view type in WPF (you can easily create your own though!). The GridView is what gives us the column-based view that you see on the screenshot.

+++

Inside of the GridView, we define three columns, one for each of the pieces of data that we wish to show. The **Header** property is used to specify the text that we would like to show for the column and then we use the **DisplayMemberBinding** property to bind the value to a property from our User class.

+++

## Templated cell content

Using the **DisplayMemberBinding** property is pretty much limited to outputting simple strings, with no custom formatting at all, but the WPF ListView is much more flexible than that. By specifying a **CellTemplate**, we take full control of how the content is rendered within the specific column cell.

+++

The GridViewColumn will use the DisplayMemberBinding as its first priority, it it's present. The second choice will be the CellTemplate property, which we'll use for this example:

+++

```XML
<Window x:Class="WpfTutorialSamples.ListView_control.ListViewGridViewCellTemplateSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListViewGridViewCellTemplateSample" Height="200" Width="400">
    <Grid>
                <ListView Margin="10" Name="lvUsers">
                        <ListView.View>
                                <GridView>
                                        <GridViewColumn Header="Name" Width="120" DisplayMemberBinding="{Binding Name}" />
                                        <GridViewColumn Header="Age" Width="50" DisplayMemberBinding="{Binding Age}" />
                                        <GridViewColumn Header="Mail" Width="150">
                                                <GridViewColumn.CellTemplate>
                                                        <DataTemplate>
                                                                <TextBlock Text="{Binding Mail}" TextDecorations="Underline" Foreground="Blue" Cursor="Hand" />
                                                        </DataTemplate>
                                                </GridViewColumn.CellTemplate>
                                        </GridViewColumn>
                                </GridView>
                        </ListView.View>
                </ListView>
        </Grid>
</Window>
```

+++

![A ListView using a GridView with a custom CellTemplate for one of the columns](http://www.wpf-tutorial.com/chapters/listview/images/listview_gridview_celltemplate.png "A ListView using a GridView with a custom CellTemplate for one of the columns")

+++

_Please notice: The Code-behind code for this example is the same as the one used for the first example in this article._

We specify a custom **CellTemplate** for the last column, where we would like to do some special formatting for the e-mail addresses. For the other columns, where we just want basic text output, we stick with the **DisplayMemberBinding**, simply because it requires way less markup.

---

# How-to: ListView with left aligned column names

In a normal ListView, the column names are left aligned, but for some reason, Microsoft decided to center the names by default in the WPF ListView. In many cases this will make your application look out-of-style compared to other Windows applications. 

+++

This is how the ListView will look in WPF by **default**:

![A ListView using a GridView for layout, with the default centered column names](http://www.wpf-tutorial.com/chapters/listview/images/listview_gridview_simple.png "A ListView using a GridView for layout, with the default centered column names")

+++

Let's try changing that to left aligned column names. Unfortunately, there are no direct properties on the GridViewColumn to control this, but fortunately that doesn't mean that it can't be changed.

Using a Style, targeted at the GridViewColumHeader, which is the element used to show the header of a GridViewColumn, we can change the HorizontalAlignment property. In this case it defaults to Center, but we can change it to Left, to accomplish what we want:

+++

```XML
<Window x:Class="WpfTutorialSamples.ListView_control.ListViewGridViewSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListViewGridViewSample" Height="200" Width="400">
    <Grid>
                <ListView Margin="10" Name="lvUsers">
                        <ListView.Resources>
                                <Style TargetType="{x:Type GridViewColumnHeader}">
                                        <Setter Property="HorizontalContentAlignment" Value="Left" />
                                </Style>
                        </ListView.Resources>
                        <ListView.View>
                                <GridView>
                                        <GridViewColumn Header="Name" Width="120" DisplayMemberBinding="{Binding Name}" />
                                        <GridViewColumn Header="Age" Width="50" DisplayMemberBinding="{Binding Age}" />
                                        <GridViewColumn Header="Mail" Width="150" DisplayMemberBinding="{Binding Mail}" />
                                </GridView>
                        </ListView.View>
                </ListView>
        </Grid>
</Window>
```

+++

![A ListView using a GridView for layout, with left aligned column header names](http://www.wpf-tutorial.com/chapters/listview/images/listview_columnheader_alignment.png "A ListView using a GridView for layout, with left aligned column header names")

The part that does all the work for us, is the Style defined in the Resources of the ListView:

+++

```XML
<Style TargetType="{x:Type GridViewColumnHeader}">
                                        <Setter Property="HorizontalContentAlignment" Value="Left" />
</Style>
```

+++

## Local or global style

By defining the Style within the control itself, it only applies to this particular ListView. In many cases you might like to make it apply to all the ListViews within the same Window/Page or perhaps even globally across the application. You can do this by either copying the style to the Window resources or the Application resources. Here's the same example, where we have applied the style to the entire Window instead of just the particular ListView:

+++

```XML
<Window x:Class="WpfTutorialSamples.ListView_control.ListViewGridViewSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListViewGridViewSample" Height="200" Width="400">
        <Window.Resources>
                <Style TargetType="{x:Type GridViewColumnHeader}">
                        <Setter Property="HorizontalContentAlignment" Value="Left" />
                </Style>
        </Window.Resources>
        <Grid>
                <ListView Margin="10" Name="lvUsers">
                        <ListView.View>
                                <GridView>
                                        <GridViewColumn Header="Name" Width="120" DisplayMemberBinding="{Binding Name}" />
                                        <GridViewColumn Header="Age" Width="50" DisplayMemberBinding="{Binding Age}" />
                                        <GridViewColumn Header="Mail" Width="150" DisplayMemberBinding="{Binding Mail}" />
                                </GridView>
                        </ListView.View>
                </ListView>
        </Grid>
</Window>
```

+++

In case you want another alignment, e.g. right alignment, you just change the value of the style like this:

```XML
<Setter Property="HorizontalContentAlignment" Value="Right" />
```

---

# ListView grouping

As we already talked about earlier, the WPF ListView is very flexible. Grouping is yet another thing that it supports out of the box, and it's both easy to use and extremely customizable. Let's jump straight into the first example, then I'll explain it and afterwards we can use the standard WPF tricks to customize the appearance even further.

For this article, I've borrowed the sample code from a previous article and then expanded on it to support grouping. It looks like this:

+++

```XML
<Window x:Class="WpfTutorialSamples.ListView_control.ListViewGroupSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListViewGroupSample" Height="300" Width="300">
    <Grid Margin="10">
        <ListView Name="lvUsers">
            <ListView.View>
                <GridView>
                    <GridViewColumn Header="Name" Width="120" DisplayMemberBinding="{Binding Name}" />
                    <GridViewColumn Header="Age" Width="50" DisplayMemberBinding="{Binding Age}" />
                </GridView>
            </ListView.View>

            <ListView.GroupStyle>
                <GroupStyle>
                    <GroupStyle.HeaderTemplate>
                        <DataTemplate>
                            <TextBlock FontWeight="Bold" FontSize="14" Text="{Binding Name}"/>
                        </DataTemplate>
                    </GroupStyle.HeaderTemplate>
                </GroupStyle>
            </ListView.GroupStyle>
        </ListView>
    </Grid>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace WpfTutorialSamples.ListView_control
{
        public partial class ListViewGroupSample : Window
        {
                public ListViewGroupSample()
                {
                        InitializeComponent();
                        List<User> items = new List<User>();
                        items.Add(new User() { Name = "John Doe", Age = 42, Sex = SexType.Male });
                        items.Add(new User() { Name = "Jane Doe", Age = 39, Sex = SexType.Female });
                        items.Add(new User() { Name = "Sammy Doe", Age = 13, Sex = SexType.Male });
                        lvUsers.ItemsSource = items;

                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Sex");
                        view.GroupDescriptions.Add(groupDescription);
                }
        }

        public enum SexType { Male, Female };

        public class User
        {
                public string Name { get; set; }

                public int Age { get; set; }

                public string Mail { get; set; }

                public SexType Sex { get; set; }
        }
}
```

+++

![A ListView with items divided into groups](http://www.wpf-tutorial.com/chapters/listview/images/listview_groups_simple.png "A ListView with items divided into groups")

+++

In XAML, I have added a GroupStyle to the ListView, in which I define a template for the header of each group. It consists of a TextBlock control, where I've used a slightly larger and bold text to show that it's a group - as we'll see later on, this can of course be customized a lot more. The TextBlock Text property is bound to a Name property, **but please be aware that this is not the Name property on the data object (in this case the User class)**. Instead, it is the name of the group, as assigned by WPF, based on the property we use to divide the objects into groups.

+++

In Code-behind, we do the same as we did before: We create a list and add some User objects to it and then we bind the list to the ListView - nothing new there, except for the new Sex property that I've added, which tells whether the user is male or female.


+++

After assigning an ItemsSource, we use this to get a CollectionView that the ListView creates for us. This specialized View instance contains a lot of possibilities, including the ability to group the items. We use this by adding a so-called PropertyGroupDescription to the GroupDescriptions of the view. This basically tells WPF to group by a specific property on the data objects, in this case the Sex property.

+++

## Customizing the group header

The above example was great for showing the basics of ListView grouping, but the look was a tad boring, so let's exploit the fact that WPF lets us define our own templates and spice things up. A common request is to be able to collapse and expand the group, and while WPF doesn't provide this behavior by default, it's somewhat easy to implement yourself. We'll do it by completely re-templating the group container.

+++

It might look a bit cumbersome, but the principles used are somewhat simple and you will see them in other situations when you customize the WPF controls.

```XML
<Window x:Class="WpfTutorialSamples.ListView_control.ListViewCollapseExpandGroupSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListViewCollapseExpandGroupSample" Height="300" Width="300">
    <Grid Margin="10">
        <ListView Name="lvUsers">
            <ListView.View>
                <GridView>
                    <GridViewColumn Header="Name" Width="120" DisplayMemberBinding="{Binding Name}" />
                    <GridViewColumn Header="Age" Width="50" DisplayMemberBinding="{Binding Age}" />
                </GridView>
            </ListView.View>

            <ListView.GroupStyle>
                <GroupStyle>
                    <GroupStyle.ContainerStyle>
                        <Style TargetType="{x:Type GroupItem}">
                            <Setter Property="Template">
                                <Setter.Value>
                                    <ControlTemplate>
                                        <Expander IsExpanded="True">
                                            <Expander.Header>
                                                <StackPanel Orientation="Horizontal">
                                                    <TextBlock Text="{Binding Name}" FontWeight="Bold" Foreground="Gray" FontSize="22" VerticalAlignment="Bottom" />
                                                    <TextBlock Text="{Binding ItemCount}" FontSize="22" Foreground="Green" FontWeight="Bold" FontStyle="Italic" Margin="10,0,0,0" VerticalAlignment="Bottom" />
                                                    <TextBlock Text=" item(s)" FontSize="22" Foreground="Silver" FontStyle="Italic" VerticalAlignment="Bottom" />
                                                </StackPanel>
                                            </Expander.Header>
                                            <ItemsPresenter />
                                        </Expander>
                                    </ControlTemplate>
                                </Setter.Value>
                            </Setter>
                        </Style>
                    </GroupStyle.ContainerStyle>
                </GroupStyle>
            </ListView.GroupStyle>
        </ListView>
    </Grid>
</Window>
```

+++

**_The Code-behind is exactly the same as used in the first example - feel free to scroll up and grab it._**

![A ListView with items divided into customized groups](http://www.wpf-tutorial.com/chapters/listview/images/listview_groups_custom.png "A ListView with items divided into customized groups")

+++

Now our groups look a bit more exciting, and they even include an expander button, that will toggle the visibility of the group items when you click it (that's why the single female user is not visible on the screenshot - I collapsed that particular group). By using the ItemCount property that the group exposes, we can even show how many items each group currently consists of.

+++

As you can see, it requires a bit more markup than we're used to, but this example also goes a bit beyond what we usually do, so that seems fair. When you read through the code, you will quickly realize that many of the lines are just common elements like style and template.

+++

## Summary

Adding grouping to the WPF ListView is very simple - all you need is a GroupStyle with a HeaderTemplate, to tell the ListView how to render a group, and a few lines of Code-behind code to tell WPF which property to group by. As you can see from the last example, the group is even very customizable, allowing you to create some really cool views, without too much work.

---


# ListView sorting

In the last chapter we saw how we could group items in the WPF ListView by accessing the View instance of the ListView and then adding a group description. Applying sorting to a ListView is just as easy, and most of the process is exactly the same. Let's try a simple example where we sort the user objects by their age:

+++

```XML
<Window x:Class="WpfTutorialSamples.ListView_control.ListViewSortingSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListViewSortingSample" Height="200" Width="300">
    <Grid Margin="10">
        <ListView Name="lvUsers">
            <ListView.View>
                <GridView>
                    <GridViewColumn Header="Name" Width="120" DisplayMemberBinding="{Binding Name}" />
                    <GridViewColumn Header="Age" Width="50" DisplayMemberBinding="{Binding Age}" />
                </GridView>
            </ListView.View>
        </ListView>
    </Grid>
</Window>
```

+++

```C#
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace WpfTutorialSamples.ListView_control
{
        public partial class ListViewSortingSample : Window
        {
                public ListViewSortingSample()
                {
                        InitializeComponent();
                        List<User> items = new List<User>();
                        items.Add(new User() { Name = "John Doe", Age = 42 });
                        items.Add(new User() { Name = "Jane Doe", Age = 39 });
                        items.Add(new User() { Name = "Sammy Doe", Age = 13 });
                        items.Add(new User() { Name = "Donna Doe", Age = 13 });
                        lvUsers.ItemsSource = items;

                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
                        view.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));
                }
        }

        public class User
        {
                public string Name { get; set; }

                public int Age { get; set; }
        }
}
```

+++

![A sorted ListView](http://www.wpf-tutorial.com/chapters/listview/images/listview_sorting_simple.png "A sorted ListView")

The XAML looks just like a previous example, where we simply have a couple of columns for displaying information about the user - nothing new here.

+++

In the Code-behind, we once again create a list of User objects, which we then assign as the ItemsSource of the ListView. Once we've done that, we use the ItemsSource property to get the CollectionView instance that the ListView automatically creates for us and which we can use to manipulate how the ListView shows our objects.

+++

With the view object in our hand, we add a new SortDescription to it, specifying that we want our list sorted by the Age property, in ascending order. As you can see from the screenshot, this works perfectly well - the list is sorted by age, instead of being in the same order as the items were added.

+++

## Multiple sort criteria

As shown in the first example, sorting is very easy, but on the screenshot you'll see that Sammy comes before Donna. They have the same age, so in this case, WPF will just use the order in which they were added. Fortunately, WPF lets us specify as many sort criteria as we want. In the example above, try changing the view-related code into something like this:

+++

```C#
CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
view.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));
view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
```

+++

![A sorted ListView, using multiple criteria](http://www.wpf-tutorial.com/chapters/listview/images/listview_sorting_multiple.png "A sorted ListView, using multiple criteria")

Now the view will be sorted using age first, and when two identical values are found, the name will be used as a secondary sorting parameter.

+++

## Summary

It's very easy to sort the contents of a ListView, as seen in the above examples, but so far, all the sorting is decided by the programmer and not the end-user. In the next article I'll give you a how-to article showing you how to let the user decide the sorting by clicking on the columns, as seen in Windows.

---

# How-to: ListView with column sorting

In the last chapter we saw how we could easily sort a ListView from Code-behind, and while this will suffice for some cases, it doesn't allow the end-user to decide on the sorting. Besides that, there was no indication on which column the ListView was sorted by. 

+++

In Windows, and in many user interfaces in general, it's common to illustrate sort directions in a list by drawing a triangle next to the column name currently used to sort by.

+++

In this how-to article, I'll give you a practical solution that gives us all of the above, but please bear in mind that some of the code here goes a bit beyond what we have learned so far - that's why it has the "how-to" label.

This article builds upon the previous one, but I'll still explain each part as we go along. Here's our goal - a ListView with column sorting, including visual indication of sort field and direction. The user simply clicks a column to sort by and if the same column is clicked again, the sort direction is reversed. 

+++

![A ListView with column sorting](http://www.wpf-tutorial.com/chapters/listview/images/listview_column_sorting.png "The goal: A ListView with column sorting")

+++

## The XAML

The first thing we need is some XAML to define our user interface. It currently looks like this:

+++

```XML
<Window x:Class="WpfTutorialSamples.ListView_control.ListViewColumnSortingSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ListViewColumnSortingSample" Height="200" Width="350">
    <Grid Margin="10">
        <ListView Name="lvUsers">
            <ListView.View>
                <GridView>
                    <GridViewColumn Width="120" DisplayMemberBinding="{Binding Name}">
                        <GridViewColumn.Header>
                            <GridViewColumnHeader Tag="Name" Click="lvUsersColumnHeader_Click">Name</GridViewColumnHeader>
                        </GridViewColumn.Header>
                    </GridViewColumn>
                    <GridViewColumn Width="80" DisplayMemberBinding="{Binding Age}">
                        <GridViewColumn.Header>
                            <GridViewColumnHeader Tag="Age" Click="lvUsersColumnHeader_Click">Age</GridViewColumnHeader>
                        </GridViewColumn.Header>
                    </GridViewColumn>
                    <GridViewColumn Width="80" DisplayMemberBinding="{Binding Sex}">
                        <GridViewColumn.Header>
                            <GridViewColumnHeader Tag="Sex" Click="lvUsersColumnHeader_Click">Sex</GridViewColumnHeader>
                        </GridViewColumn.Header>
                    </GridViewColumn>
                </GridView>
            </ListView.View>
        </ListView>
    </Grid>
</Window>
```

+++

Notice how I have specified headers for each of the columns using an actual GridViewColumnHeader element instead of just specifying a string. This is done so that I may set additional properties, in this case the **Tag** property as well as the **Click** event.

+++

The **Tag** property is used to hold the field name that will be used to sort by, if this particular column is clicked. This is done in the _lvUsersColumnHeader_Click_ event that each of the columns subscribes to.

That was the key concepts of the XAML. Besides that, we bind to our Code-behind properties Name, Age and Sex, which we'll discuss now.

+++

## The Code-behind

In Code-behind, there are quite a few things happening. I use a total of three classes, which you would normally divide up into individual files, but for convenience, I have kept them in the same file, giving us a total of ~100 lines. First the code and then I'll explain how it works:

+++

```C#
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfTutorialSamples.ListView_control
{
        public partial class ListViewColumnSortingSample : Window
        {
                private GridViewColumnHeader listViewSortCol = null;
                private SortAdorner listViewSortAdorner = null;

                public ListViewColumnSortingSample()
                {
                        InitializeComponent();
                        List<User> items = new List<User>();
                        items.Add(new User() { Name = "John Doe", Age = 42, Sex = SexType.Male });
                        items.Add(new User() { Name = "Jane Doe", Age = 39, Sex = SexType.Female });
                        items.Add(new User() { Name = "Sammy Doe", Age = 13, Sex = SexType.Male });
                        items.Add(new User() { Name = "Donna Doe", Age = 13, Sex = SexType.Female });
                        lvUsers.ItemsSource = items;
                }

                private void lvUsersColumnHeader_Click(object sender, RoutedEventArgs e)
                {
                        GridViewColumnHeader column = (sender as GridViewColumnHeader);
                        string sortBy = column.Tag.ToString();
                        if(listViewSortCol != null)
                        {
                                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                                lvUsers.Items.SortDescriptions.Clear();
                        }

                        ListSortDirection newDir = ListSortDirection.Ascending;
                        if(listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                                newDir = ListSortDirection.Descending;

                        listViewSortCol = column;
                        listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
                        AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
                        lvUsers.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
                }
        }

        public enum SexType { Male, Female };

        public class User
        {
                public string Name { get; set; }

                public int Age { get; set; }

                public string Mail { get; set; }

                public SexType Sex { get; set; }
        }

        public class SortAdorner : Adorner
        {
                private static Geometry ascGeometry =
                        Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

                private static Geometry descGeometry =
                        Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

                public ListSortDirection Direction { get; private set; }

                public SortAdorner(UIElement element, ListSortDirection dir)
                        : base(element)
                {
                        this.Direction = dir;
                }

                protected override void OnRender(DrawingContext drawingContext)
                {
                        base.OnRender(drawingContext);

                        if(AdornedElement.RenderSize.Width < 20)
                                return;

                        TranslateTransform transform = new TranslateTransform
                                (
                                        AdornedElement.RenderSize.Width - 15,
                                        (AdornedElement.RenderSize.Height - 5) / 2
                                );
                        drawingContext.PushTransform(transform);

                        Geometry geometry = ascGeometry;
                        if(this.Direction == ListSortDirection.Descending)
                                geometry = descGeometry;
                        drawingContext.DrawGeometry(Brushes.Black, null, geometry);

                        drawingContext.Pop();
                }
        }
}
```

+++

Allow me to start from the bottom and then work my way up while explaining what happens. The last class in the file is an Adorner class called **SortAdorner**. All this little class does is to draw a triangle, either pointing up or down, depending on the sort direction. WPF uses the concept of adorners to allow you to paint stuff over other controls, and this is exactly what we want here: The ability to draw a sorting triangle on top of our ListView column header.

+++

The **SortAdorner** works by defining two **Geometry** objects, which are basically used to describe 2D shapes - in this case a triangle with the tip pointing up and one with the tip pointing down. The Geometry.Parse() method uses the list of points to draw the triangles, which will be explained more thoroughly in a later article.

+++

The **SortAdorner** is aware of the sort direction, because it needs to draw the proper triangle, but is not aware of the field that we order by - this is handled in the UI layer.

The **User** class is just a basic information class, used to contain information about a user. Some of this information is used in the UI layer, where we bind to the Name, Age and Sex properties.

+++

In the Window class, we have two methods: The constructor where we build a list of users and assign it to the ItemsSource of our ListView, and then the more interesting click event handler that will be hit when the user clicks a column. In the top of the class, we have defined two private variables: _listViewSortCol_ and _listViewSortAdorner_. These will help us keep track of which column we're currently sorting by and the adorner we placed to indicate it.

+++

In the lvUsersColumnHeader_Click event handler, we start off by getting a reference to the column that the user clicked. With this, we can decide which property on the User class to sort by, simply by looking at the Tag property that we defined in XAML. We then check if we're already sorting by a column - if that is the case, we remove the adorner and clear the current sort descriptions.

+++

After that, we're ready to decide the direction. The default is ascending, but we do a check to see if we're already sorting by the column that the user clicked - if that is the case, we change the direction to descending.

In the end, we create a new SortAdorner, passing in the column that it should be rendered on, as well as the direction. We add this to the AdornerLayer of the column header, and at the very end, we add a SortDescription to the ListView, to let it know which property to sort by and in which direction.

+++

## Summary

Congratulations, you now have a fully sortable ListView with visual indication of sort column and direction. In case you want to know more about some of the concepts used in this article, like data binding, geometry or ListViews in general, then please check out some of the other articles, where each of the subjects are covered in depth.

---

# ListView filtering

We've already done several different things with the ListView, like grouping and sorting, but another very useful ability is filtering. Obviously, you could just limit the items you add to the ListView in the first place, but often you would need to filter the ListView dynamically, in runtime, usually based on a user entered filter string. Luckily for us, the view mechanisms of the ListView also make it easy to do just that, like we saw it with sorting and grouping.

+++

Filtering is actually quite easy to do:

```XML
<Window x:Class="WpfTutorialSamples.ListView_control.FilteringSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="FilteringSample" Height="200" Width="300">
    <DockPanel Margin="10">
        <TextBox DockPanel.Dock="Top" Margin="0,0,0,10" Name="txtFilter" TextChanged="txtFilter_TextChanged" />
        <ListView Name="lvUsers">
            <ListView.View>
                <GridView>
                    <GridViewColumn Header="Name" Width="120" DisplayMemberBinding="{Binding Name}" />
                    <GridViewColumn Header="Age" Width="50" DisplayMemberBinding="{Binding Age}" />
                </GridView>
            </ListView.View>
        </ListView>
    </DockPanel>
</Window>
```

+++

```XML
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace WpfTutorialSamples.ListView_control
{
        public partial class FilteringSample : Window
        {
                public FilteringSample()
                {
                        InitializeComponent();
                        List<User> items = new List<User>();
                        items.Add(new User() { Name = "John Doe", Age = 42 });
                        items.Add(new User() { Name = "Jane Doe", Age = 39 });
                        items.Add(new User() { Name = "Sammy Doe", Age = 13 });
                        items.Add(new User() { Name = "Donna Doe", Age = 13 });
                        lvUsers.ItemsSource = items;

                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
                        view.Filter = UserFilter;
                }

                private bool UserFilter(object item)
                {
                        if(String.IsNullOrEmpty(txtFilter.Text))
                                return true;
                        else
                                return ((item as User).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
                {
                        CollectionViewSource.GetDefaultView(lvUsers.ItemsSource).Refresh();
                }
        }

        public enum SexType { Male, Female };

        public class User
        {
                public string Name { get; set; }

                public int Age { get; set; }

                public string Mail { get; set; }

                public SexType Sex { get; set; }
        }
}
```

+++

![A filtered ListView](http://www.wpf-tutorial.com/chapters/listview/images/listview_filtering_simple.png "A filtered ListView")

The XAML part is pretty simple: We have a TextBox, where the user can enter a search string, and then a ListView to show the result in.

+++

In Code-behind, we start off by adding some User objects to the ListView, just like we did in previous examples. The interesting part happens in the last two lines of the constructor, where we obtain a reference to the **CollectionView** instance for the ListView and then assign a delegate to the **Filter** property. This delegate points to the function called **UserFilter**, which we have implemented just below. It takes each item as the first (and only) parameter and then returns a boolean value that indicates whether or not the given item should be visible on the list.

+++

In the **UserFilter()** method, we take a look at the TextBox control (txtFilter), to see if it contains any text - if it does, we use it to check whether or not the name of the User (which is the property we have decided to filter on) contains the entered string, and then return true or false depending on that. If the TextBox is empty, we return true, because in that case we want all the items to be visible.

+++

The txtFilter_TextChanged event is also important. Each time the text changes, we get a reference to the View object of the ListView and then call the Refresh() method on it. This ensures that the Filter delegate is called each time the user changes the value of the search/filter string text box.

+++

## Summary

This was a pretty simple implementation, but since you get access to each item, in this case of the User class, you can do any sort of custom filtering that you like, since you have access to all of the data about each of the items in the list. For instance, the above example could easily be changed to filter on age, by looking at the Age property instead of the Name property, or you could modify it to look at more than one property, e.g. to filter out users with an age below X AND a name that doesn't contain "Y".
