# The TextBlock control

_TextBlock is not a control, per se, since it doesn't inherit from the Control class, but it's used much like any other control in the WPF framework, so we'll call it a control to keep things simple._

+++

The **TextBlock** control is one of the most fundamental controls in WPF, yet it's very useful. It allows you to put text on the screen, much like a Label control does, but in a simpler and less resource demanding way. A common understanding is that a Label is for short, one-line texts (but may include e.g. an image), while the TextBlock works very well for multiline strings as well, but can only contain text (strings). Both the Label and the TextBlock offers their own unique advantages, so what you should use very much depends on the situation.

+++

We already used a TextBlock control in the "Hello, WPF!" article, but for now, let's have a look at the TextBlock in its simplest form:

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockSample" Height="100" Width="200">
    <Grid>
                <TextBlock>This is a TextBlock</TextBlock>
    </Grid>
</Window>
```

+++

![A simple TextBlock control](http://www.wpf-tutorial.com/chapters/basic-controls/images/textblock_simple.png "A simple TextBlock control")

+++

That's as simple as it comes and if you have read the previous chapters of this tutorial, then there should be nothing new here. The text between the TextBlock is simply a shortcut for setting the Text property of the TextBlock.

+++

For the next example, let's try a longer text to show how the TextBlock deals with that. I've also added a bit of margin, to make it look just a bit better:

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockSample" Height="100" Width="200">
    <Grid>
                <TextBlock Margin="10">This is a TextBlock control and it comes with a very long text</TextBlock>
    </Grid>
</Window>
```

+++

![A simple TextBlock control with text that's too long to fit](http://www.wpf-tutorial.com/chapters/basic-controls/images/textblock_long_text.png "A simple TextBlock control with text that's too long to fit")

---

## Dealing with long strings

As you will soon realize from the screenshot, the TextBlock is perfectly capable of dealing with long, multiline texts, but it will not do anything by default. In this case the text is too long to be rendered inside the window, so WPF renders as much of the text as possible and then just stops.

+++

Fortunately, there are several ways of dealing with this. In the next example I'll show you all of them, and then I'll explain each of them afterwards:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockSample" Height="200" Width="250">
    <StackPanel>
                <TextBlock Margin="10" Foreground="Red">
                        This is a TextBlock control<LineBreak />
                        with multiple lines of text.
                </TextBlock>
                <TextBlock Margin="10" TextTrimming="CharacterEllipsis" Foreground="Green">
                        This is a TextBlock control with text that may not be rendered completely, which will be indicated with an ellipsis.
                </TextBlock>
                <TextBlock Margin="10" TextWrapping="Wrap" Foreground="Blue">
                        This is a TextBlock control with automatically wrapped text, using the TextWrapping property.
                </TextBlock>
        </StackPanel>
</Window>
```

+++

![A TextBlock control showing several ways to deal with long strings](http://www.wpf-tutorial.com/chapters/basic-controls/images/textblock_long_text_solutions.png "A TextBlock control showing several ways to deal with long strings")

+++

So, we have three TextBlock controls, each with a different color (using the Foreground property) for an easier overview. They all handle the fact that their text content is too long in different ways:

+++

The <span style="color: red;">red TextBlock</span> uses a **LineBreak** tag to manually break the line at a designated location. This gives you absolute control over where you want the text to break onto a new line, but it's not very flexible for most situations. If the user makes the window bigger, the text will still wrap at the same position, even though there may now be room enough to fit the entire text onto one line.

+++

The <span style="color: green;">green TextBlock</span> uses the **TextTrimming** property with the value **CharacterEllipsis** to make the TextBlock show an ellipsis (...) when it can't fit any more text into the control. This is a common way of showing that there's more text, but not enough room to show it. This is great when you have text that might be too long but you absolutely don't want it to use more than one line. As an alternative to **CharacterEllipsis** you may use **WordEllipsis**, which will trim the text at the end of the last possible word instead of the last possible character, preventing that a word is only shown in part.

+++

The <span style="color: blue;">blue TextBlock</span> uses the **TextWrapping** property with the value **Wrap**, to make the TextBlock wrap to the next line whenever it can't fit anymore text into the previous line. Contrary to the first TextBlock, where we manually define where to wrap the text, this happens completely automatic and even better: It's also automatically adjusted as soon as the TextBlock get more or less space available. Try making the window in the example bigger or smaller and you will see how the wrapping is updated to match the situation.

+++

This was all about dealing with simple strings in the TextBlock. In the next chapter, we'll look into some of the more advanced functionality of the TextBlock, which allows us to create text of various styles within the TextBlock and much more.

---

# The TextBlock control - Inline formatting

In the last article we looked at the core functionality of the TextBlock control: Displaying a simple string and wrapping it if necessary. We even used another color than the default for rendering the text, but what if you wanted to do more than just define a static color for all the text in the TextBlock?

+++

Luckily the TextBlock control supports inline content. These small control-like constructs all inherit from the Inline class, which means that they can be rendered inline, as a part of a larger text. As of writing, the supported elements include AnchoredBlock, Bold, Hyperlink, InlineUIContainer, Italic, LineBreak, Run, Span, and Underline. In the following examples, we'll have a look at most of them.

---

## Bold, Italic and Underline

These are probably the simplest types of inline elements. The names should tell you a lot about what they do, but we'll still give you a quick example on how to use them:

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockInlineSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockInlineSample" Height="100" Width="300">
    <Grid>
                <TextBlock Margin="10" TextWrapping="Wrap">
                        TextBlock with <Bold>bold</Bold>, <Italic>italic</Italic> and <Underline>underlined</Underline> text.
                </TextBlock>
    </Grid>
</Window>
```

+++

![A TextBlock control with inline bold, italic and underlined elements](http://www.wpf-tutorial.com/chapters/basic-controls/images/textblock_inline_bold.png "A TextBlock control with inline bold, italic and underlined elements")

+++

Much like with HTML, you just surround your text with a Bold tag to get bold text and so on. This makes it very easy to create and display diverse text in your applications.

All three of these tags are just child classes of the Span element, each setting a specific property on the Span element to create the desired effect. For instance, the Bold tag just sets the FontWeight property on the underlying Span element, the Italic element sets the FontStyle and so on.

---

## LineBreak

Simply inserts a line break into the text. Please see the previous chapter for an example where we use the LineBreak element.

---

## Hyperlink

The Hyperlink element allows you to have links in your text. It's rendered with a style that suits your current Windows theme, which will usually be some sort of underlined blue text with a red hover effect and a hand mouse cursor. You can use the NavigateUri property to define the URL that you wish to navigate to. Here's an example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockHyperlinkSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockHyperlinkSample" Height="100" Width="300">
        <Grid>
                <TextBlock Margin="10" TextWrapping="Wrap">
                        This text has a <Hyperlink RequestNavigate="Hyperlink_RequestNavigate" NavigateUri="https://www.google.com">link</Hyperlink> in it.
                </TextBlock>
        </Grid>
</Window>
```

+++

![A TextBlock control using the Hyperlink element to create a clickable link](http://www.wpf-tutorial.com/chapters/basic-controls/images/textblock_inline_hyperlink.png "A TextBlock control using the Hyperlink element to create a clickable link")

+++

The Hyperlink is also used inside of WPF Page's, where it can be used to navigate between pages. In that case, you won't have to specifically handle the RequestNavigate event, like we do in the example, but for launching external URL's from a regular WPF application, we need a bit of help from this event and the Process class. We subscribe to the RequestNavigate event, which allows us to launch the linked URL in the users default browser with a simple event handler like this one in the code behind file:

+++

```C#
private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
{
        System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
}
```

---

## Run

The Run element allows you to style a string using all the available properties of the Span element, but while the Span element may contain other inline elements, a Run element may only contain plain text. This makes the Span element more flexible and therefore the logical choice in most cases.

---

## Span

The Span element doesn't have any specific rendering by default, but allows you to set almost any kind of specific rendering, including font size, style and weight, background and foreground colors and so on. The great thing about the Span element is that it allows for other inline elements inside of it, making it easy to do even advanced combinations of text and style. In the following example, I have used many Span elements to show you some of the many possibilities when using inline Span elements:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockSpanSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockSpanSample" Height="100" Width="300">
    <Grid>
                <TextBlock Margin="10" TextWrapping="Wrap">
                        This <Span FontWeight="Bold">is</Span> a
                        <Span Background="Silver" Foreground="Maroon">TextBlock</Span>
                        with <Span TextDecorations="Underline">several</Span>
                        <Span FontStyle="Italic">Span</Span> elements,
                        <Span Foreground="Blue">
                                using a <Bold>variety</Bold> of <Italic>styles</Italic>
                        </Span>.
                </TextBlock>
        </Grid>
</Window>
```

+++

![A TextBlock control using a variety of differently styled Span elements for custom text formatting](http://www.wpf-tutorial.com/chapters/basic-controls/images/textblock_inline_span.png "A TextBlock control using a variety of differently styled Span elements for custom text formatting")

+++

So as you can see, if none of the other elements doesn't make sense in your situation or if you just want a blank canvas when starting to format your text, the Span element is a great choice.

---

## Formatting text from C#/Code-Behind

As you can see, formatting text through XAML is very easy, but in some cases, you might prefer or even need to do it from your C#/Code-Behind file. This is a bit more cumbersome, but here's an example on how you may do it:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBlockCodeBehindSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBlockCodeBehindSample" Height="100" Width="300">
    <Grid></Grid>
</Window>
```

+++

```C#
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfTutorialSamples.Basic_controls
{
        public partial class TextBlockCodeBehindSample : Window
        {
                public TextBlockCodeBehindSample()
                {
                        InitializeComponent();
                        TextBlock tb = new TextBlock();
                        tb.TextWrapping = TextWrapping.Wrap;
                        tb.Margin = new Thickness(10);
                        tb.Inlines.Add("An example on ");
                        tb.Inlines.Add(new Run("the TextBlock control ") { FontWeight = FontWeights.Bold });
                        tb.Inlines.Add("using ");
                        tb.Inlines.Add(new Run("inline ") { FontStyle = FontStyles.Italic });
                        tb.Inlines.Add(new Run("text formatting ") { Foreground = Brushes.Blue });
                        tb.Inlines.Add("from ");
                        tb.Inlines.Add(new Run("Code-Behind") { TextDecorations = TextDecorations.Underline });
                        tb.Inlines.Add(".");
                        this.Content = tb;
                }
        }
}
```

+++

![A TextBlock control with custom text formatting generated with C# code instead of XAML](http://www.wpf-tutorial.com/chapters/basic-controls/images/textblock_inline_codebehind.png "A TextBlock control with custom text formatting generated with C# code instead of XAML")

+++

It's great to have the possibility, and it can be necessary to do it like this in some cases, but this example will probably make you appreciate XAML even more.

---

# The Label control

The Label control, in its most simple form, will look very much like the TextBlock which we used in another article. You will quickly notice though that instead of a Text property, the Label has a Content property. The reason for that is that the Label can host any kind of control directly inside of it, instead of just text. This content can be a string as well though, as you will see in this first and very basic example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.LabelControlSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="LabelControlSample" Height="100" Width="200">
    <Grid>
                <Label Content="This is a Label control." />
        </Grid>
</Window>
```

+++

![A simple Label control](http://www.wpf-tutorial.com/chapters/basic-controls/images/label_simple.png "A simple Label control")

+++

Another thing you might notice is the fact that the Label, by default, has a bit of padding, allowing the text to be rendered a few pixels away from the top, left corner. This is not the case for the TextBlock control, where you will have to specify it manually.

In a simple case like this, where the content is simply a string, the Label will actually create a TextBlock internally and show your string in that.

---

## The Label control vs. the TextBlock control

So why use a Label at all then? Well, there are a few important differences between the Label and the TextBlock. The TextBlock only allows you to render a text string, while the Label also allows you to:

*   Specify a border
*   Render other controls, e.g. an image
*   Use templated content through the ContentTemplate property
*   **Use access keys to give focus to related controls**

+++

The last bullet point is actually one of the main reasons for using a Label over the TextBlock control. Whenever you just want to render simple text, you should use the TextBlock control, since it's lighter and performs better than the Label in most cases.

---

## Label and Access keys (mnemonics)

In Windows and other operating systems as well, it's common practice that you can access controls in a dialog by holding down the [Alt] key and then pressing a character which corresponds to the control that you wish to access. The character to press will be highlighted when you hold down the [Alt] key. TextBlock controls doesn't support this functionality, but the Label does, so for control labels, the Label control is usually an excellent choice. 

+++

Let's look at an example of it in action:

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.LabelControlSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="LabelControlSample" Height="180" Width="250">
        <StackPanel Margin="10">
                <Label Content="_Name:" Target="{Binding ElementName=txtName}" />
                <TextBox Name="txtName" />
                <Label Content="_Mail:" Target="{Binding ElementName=txtMail}" />
                <TextBox Name="txtMail" />
        </StackPanel>
</Window>
```

+++

![Label controls using access keys](http://www.wpf-tutorial.com/chapters/basic-controls/images/label_access_keys.png "Label controls using access keys")

The screenshot shows our sample dialog as it looks when the Alt key is pressed. Try running it, holding down the [Alt] key and then pressing N and M. You will see how focus is moved between the two textboxes.

+++

So, there's several new concepts here. First of all, we define the access key by placing an underscore (_) before the character. It doesn't have to be the first character, it can be before any of the characters in your label content. The common practice is to use the first character that's not already used as an access key for another control.

+++

We use the **Target** property to connect the Label and the designated control. We use a standard WPF binding for this, using the **ElementName** property, all of which we will describe later on in this tutorial. The binding is based on the name of the control, so if you change this name, you will also have to remember to change the binding.

---

## Using controls as Label content

As already mentioned, the Label control allows you to host other controls, while still keeping the other benefits. Let's try an example where we have both an image and a piece of text inside the Label, while also having an access key for each of the labels:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.LabelControlAdvancedSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="LabelControlAdvancedSample" Height="180" Width="250">
        <StackPanel Margin="10">
                <Label Target="{Binding ElementName=txtName}">
                        <StackPanel Orientation="Horizontal">
                                <Image Source="http://cdn1.iconfinder.com/data/icons/fatcow/16/bullet_green.png" />
                                <AccessText Text="_Name:" />
                        </StackPanel>
                </Label>
                <TextBox Name="txtName" />
                <Label Target="{Binding ElementName=txtMail}">
                        <StackPanel Orientation="Horizontal">
                                <Image Source="http://cdn1.iconfinder.com/data/icons/fatcow/16/bullet_blue.png" />
                                <AccessText Text="_Mail:" />
                        </StackPanel>
                </Label>
                <TextBox Name="txtMail" />
        </StackPanel>
</Window>
```

+++

![Label controls using access keys](http://www.wpf-tutorial.com/chapters/basic-controls/images/label_with_image.png "Label controls using access keys and child controls with an image")

+++

This is just an extended version of the previous example - instead of a simple text string, our Label will now host both and image and a piece of text (inside the AccessText control, which allows us to still use an access key for the label). Both controls are inside a horizontal StackPanel, since the Label, just like any other ContentControl derivate, can only host one direct child control.

+++

_The Image control, described later in this tutorial, uses a remote image - this is ONLY for demonstrational purposes and is NOT a good idea for most real life applications._

---

## Summary

In most situations, the Label control does exactly what the name implies: It acts as a text label for another control. This is the primary purpose of it. For most other cases, you should probably use a TextBlock control or one of the other text containers that WPF offers.

---

# The TextBox control

The TextBox control is the most basic text-input control found in WPF, allowing the end-user to write plain text, either on a single line, for dialog input, or in multiple lines, like an editor.

---

## Single-line TextBox

The TextBox control is such a commonly used thing that you actually don't have to use any properties on it, to have a full-blown editable text field. Here's a barebone example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBoxSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBoxSample" Height="80" Width="250">
    <StackPanel Margin="10">
                <TextBox />
        </StackPanel>
</Window>
```

+++

![A simple TextBox control](http://www.wpf-tutorial.com/chapters/basic-controls/images/textbox_simple.png "A simple TextBox control")

+++

That's all you need to get a text field. I added the text after running the sample and before taking the screenshot, but you can do it through markup as well, to pre-fill the textbox, using the Text property:

```XML
<TextBox Text="Hello, world!" />
```

+++

Try right-clicking in the TextBox. You will get a menu of options, allowing you to use the TextBox with the Windows Clipboard. The default keyboard shortcuts for undoing and redoing (Ctrl+Z and Ctrl+Y) should also work, and all of this functionality you get for free!

---

## Multi-line TextBox

If you run the above example, you will notice that the TextBox control by default is a single-line control. Nothing happens when you press Enter and if you add more text than what can fit on a single line, the control just scrolls. However, making the TextBox control into a multi-line editor is very simple:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBoxSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBoxSample" Height="160" Width="280">
    <Grid Margin="10">
                <TextBox AcceptsReturn="True" TextWrapping="Wrap" />
        </Grid>
</Window>
```

+++

![A TextBox control with multiple lines of text](http://www.wpf-tutorial.com/chapters/basic-controls/images/textbox_multiline.png "A TextBox control with multiple lines of text")

+++

I have added two properties: The AcceptsReturn makes the TextBox into a multi-line control by allowing the use of the Enter/Return key to go to the next line, and the TextWrapping property, which will make the text wrap automatically when the end of a line is reached.

---

## Spellcheck with TextBox

As an added bonus, the TextBox control actually comes with automatic spell checking for English and a couple of other languages (as of writing, English, French, German, and Spanish languages are supported).

It works much like in Microsoft Word, where spelling errors are underlined and you can right-click it for suggested alternatives. Enabling spell checking is very easy:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBoxSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBoxSample" Height="160" Width="280">
    <Grid Margin="10">
                <TextBox AcceptsReturn="True" TextWrapping="Wrap" SpellCheck.IsEnabled="True" Language="en-US" />
        </Grid>
</Window>
```

+++

![A TextBox control with automatic spell checking enabled](http://www.wpf-tutorial.com/chapters/basic-controls/images/textbox_spellcheck.png "A TextBox control with automatic spell checking enabled")

+++

We have used the previous, multi-line textbox example as the basis and then I have added two new properties: The attached property from the SpellCheck class called IsEnabled, which simply enables spell checking on the parent control, and the Language property, which instructs the spell checker which language to use.

---

## Working with TextBox selections

Just like any other editable control in Windows, the TextBox allows for selection of text, e.g. to delete an entire word at once or to copy a piece of the text to the clipboard. The WPF TextBox has several properties for working with selected text, all of them which you can read or even modify. In the next example, we will be reading these properties:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.TextBoxSelectionSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="TextBoxSelectionSample" Height="150" Width="300">
        <DockPanel Margin="10">
                <TextBox SelectionChanged="TextBox_SelectionChanged" DockPanel.Dock="Top" />
                <TextBox Name="txtStatus" AcceptsReturn="True" TextWrapping="Wrap" IsReadOnly="True" />

        </DockPanel>
</Window>
```

+++

The example consists of two TextBox controls: One for editing and one for outputting the current selection status to. For this, we set the IsReadOnly property to true, to prevent editing of the status TextBox. We subscribe the SelectionChanged event on the first TextBox, which we handle in the Code-behind:

+++

```C#
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfTutorialSamples.Basic_controls
{
        public partial class TextBoxSelectionSample : Window
        {
                public TextBoxSelectionSample()
                {
                        InitializeComponent();
                }

                private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
                {
                        TextBox textBox = sender as TextBox;
                        txtStatus.Text = "Selection starts at character #" + textBox.SelectionStart + Environment.NewLine;
                        txtStatus.Text += "Selection is " + textBox.SelectionLength + " character(s) long" + Environment.NewLine;
                        txtStatus.Text += "Selected text: '" + textBox.SelectedText + "'";
                }
        }
}
```

+++

![A TextBox control with selection status](http://www.wpf-tutorial.com/chapters/basic-controls/images/textbox_selection.png "A TextBox control with selection status")

+++

We use three interesting properties to accomplish this:

**SelectionStart** , which gives us the current cursor position or if there's a selection: Where it starts.

**SelectionLength** , which gives us the length of the current selection, if any. Otherwise it will just return 0.

**SelectedText** , which gives us the currently selected string if there's a selection. Otherwise an empty string is returned.

---

## Modifying the selection

All of these properties are both readable and writable, which means that you can modify them as well. For instance, you can set the SelectionStart and SelectionLength properties to select a custom range of text, or you can use the SelectedText property to insert and select a string. Just remember that the TextBox has to have focus, e.g. by calling the Focus() method first, for this to work.

---

# The CheckBox control

The CheckBox control allows the end-user to toggle an option on or off, usually reflecting a Boolean value in the Code-behind. Let's jump straight into an example, in case you're not sure how a CheckBox looks:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.CheckBoxSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="CheckBoxSample" Height="140" Width="250">
    <StackPanel Margin="10">
                <Label FontWeight="Bold">Application Options</Label>
                <CheckBox>Enable feature ABC</CheckBox>
                <CheckBox IsChecked="True">Enable feature XYZ</CheckBox>
                <CheckBox>Enable feature WWW</CheckBox>
        </StackPanel>
</Window>
```

+++

![A simple CheckBox control](http://www.wpf-tutorial.com/chapters/basic-controls/images/checkbox_simple.png "A simple CheckBox control")

+++

As you can see, the CheckBox is very easy to use. On the second CheckBox, I use the IsChecked property to have it checked by default, but other than that, no properties are needed to use it. The IsChecked property should also be used from Code-behind if you want to check whether a certain CheckBox is checked or not.

---

## Custom content

The CheckBox control inherits from the ContentControl class, which means that it can take custom content and display next to it. If you just specify a piece of text, like I did in the example above, WPF will put it inside a TextBlock control and display it, but this is just a shortcut to make things easier for you. You can use any type of control inside of it, as we'll see in the next example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.CheckBoxSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="CheckBoxSample" Height="140" Width="250">
    <StackPanel Margin="10">
                <Label FontWeight="Bold">Application Options</Label>
                <CheckBox>
                        <TextBlock>
                                Enable feature <Run Foreground="Green" FontWeight="Bold">ABC</Run>
                        </TextBlock>
                </CheckBox>
                <CheckBox IsChecked="True">
                        <WrapPanel>
                                <TextBlock>
                                        Enable feature <Run FontWeight="Bold">XYZ</Run>
                                </TextBlock>
                                <Image Source="/WpfTutorialSamples;component/Images/question.png" Width="16" Height="16" Margin="5,0" />
                        </WrapPanel>
                </CheckBox>
                <CheckBox>
                        <TextBlock>
                                Enable feature <Run Foreground="Blue" TextDecorations="Underline" FontWeight="Bold">WWW</Run>
                        </TextBlock>
                </CheckBox>
        </StackPanel>
</Window>
```

+++

![A CheckBox control with custom content](http://www.wpf-tutorial.com/chapters/basic-controls/images/checkbox_custom_content.png "A CheckBox control with custom content")

+++

As you can see from the sample markup, you can do pretty much whatever you want with the content. On all three check boxes, I do something differently with the text, and on the middle one I even throw in an Image control. By specifying a control as the content, instead of just text, we get much more control of the appearance, and the cool thing is that no matter which part of the content you click on, it will activate the CheckBox and toggle it on or off.

---

## The IsThreeState property

As mentioned, the CheckBox usually corresponds to a boolean value, which means that it only has two states: true or false (on or off). However, since a boolean data type might be nullable, effectively allowing for a third option (true, false or null), the CheckBox control can also support this case. By setting the IsThreeState property to true, the CheckBox will get a third state called "the indeterminate state".

+++

A common usage for this is to have a "Enable all" CheckBox, which can control a set of child checkboxes, as well as show their collective state. Our example shows how you may create a list of features that can be toggled on and off, with a common "Enable all" CheckBox in the top:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.CheckBoxThreeStateSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="CheckBoxThreeStateSample" Height="170" Width="300">
        <StackPanel Margin="10">
                <Label FontWeight="Bold">Application Options</Label>
                <StackPanel Margin="10,5">
                        <CheckBox IsThreeState="True" Name="cbAllFeatures" Checked="cbAllFeatures_CheckedChanged" Unchecked="cbAllFeatures_CheckedChanged">Enable all</CheckBox>
                        <StackPanel Margin="20,5">
                                <CheckBox Name="cbFeatureAbc" Checked="cbFeature_CheckedChanged" Unchecked="cbFeature_CheckedChanged">Enable feature ABC</CheckBox>
                                <CheckBox Name="cbFeatureXyz" IsChecked="True" Checked="cbFeature_CheckedChanged" Unchecked="cbFeature_CheckedChanged">Enable feature XYZ</CheckBox>
                                <CheckBox Name="cbFeatureWww" Checked="cbFeature_CheckedChanged" Unchecked="cbFeature_CheckedChanged">Enable feature WWW</CheckBox>
                        </StackPanel>
                </StackPanel>
        </StackPanel>
</Window>
```

+++

```C#
using System;
using System.Windows;

namespace WpfTutorialSamples.Basic_controls
{
        public partial class CheckBoxThreeStateSample : Window
        {
                public CheckBoxThreeStateSample()
                {
                        InitializeComponent();
                }


                private void cbAllFeatures_CheckedChanged(object sender, RoutedEventArgs e)
                {
                        bool newVal = (cbAllFeatures.IsChecked == true);
                        cbFeatureAbc.IsChecked = newVal;
                        cbFeatureXyz.IsChecked = newVal;
                        cbFeatureWww.IsChecked = newVal;
                }

                private void cbFeature_CheckedChanged(object sender, RoutedEventArgs e)
                {
                        cbAllFeatures.IsChecked = null;
                        if((cbFeatureAbc.IsChecked == true) && (cbFeatureXyz.IsChecked == true) && (cbFeatureWww.IsChecked == true))
                                cbAllFeatures.IsChecked = true;
                        if((cbFeatureAbc.IsChecked == false) && (cbFeatureXyz.IsChecked == false) && (cbFeatureWww.IsChecked == false))
                                cbAllFeatures.IsChecked = false;
                }

        }
}
```

+++

![A three state CheckBox control in the inderminate state](http://www.wpf-tutorial.com/chapters/basic-controls/images/checkbox_three_state_intermediate.png "A three state CheckBox control in the inderminate state") 

+++

![A three state CheckBox control in the checked state](http://www.wpf-tutorial.com/chapters/basic-controls/images/checkbox_three_state_checked.png "A three state CheckBox control in the checked state") 

+++

![A three state CheckBox control in the unchecked state](http://www.wpf-tutorial.com/chapters/basic-controls/images/checkbox_three_state_unchecked.png "A three state CheckBox control in the unchecked state")

+++

This example works from two different angles: If you check or uncheck the "Enable all" CheckBox, then all of the child check boxes, each representing an application feature in our example, is either checked or unchecked. It also works the other way around though, where checking or unchecking a child CheckBox affects the "Enable all" CheckBox state: If they are all checked or unchecked, then the "Enable all" CheckBox gets the same state - otherwise the value will be left with a null, which forces the CheckBox into the indeterminate state.

+++

All of this behavior can be seen on the screenshots above, and is achieved by subscribing to the Checked and Unchecked events of the CheckBox controls. In a real world example, you would likely bind the values instead, but this example shows the basics of using the IsThreeState property to create a "Toggle all" effect.

---

# The RadioButton control

The RadioButton control allows you to give your user a list of possible options, with only one of them selected at the same time. You can achieve the same effect, using less space, with the ComboBox control, but a set of radio buttons tend to give the user a better overview of the options they have.

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.RadioButtonSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="RadioButtonSample" Height="150" Width="250">
        <StackPanel Margin="10">
                <Label FontWeight="Bold">Are you ready?</Label>
                <RadioButton>Yes</RadioButton>
                <RadioButton>No</RadioButton>
                <RadioButton IsChecked="True">Maybe</RadioButton>
        </StackPanel>
</Window>
```

+++

![A simple RadioButton control](http://www.wpf-tutorial.com/chapters/basic-controls/images/radiobuttons_simple.png "A simple RadioButton control")

+++

All we do is add a Label with a question, and then three radio buttons, each with a possible answer. We define a default option by using the IsChecked property on the last RadioButton, which the user can change simply by clicking on one of the other radio buttons. **This is also the property you would want to use from Code-behind to check if a RadioButton is checked or not.**

---

## RadioButton groups

If you try running the example above, you will see that, as promised, only one RadioButton can be checked at the same time. But what if you want several groups of radio buttons, each with their own, individual selection? This is what the **GroupName** property comes into play, which allows you to specify which radio buttons belong together. Here's an example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.RadioButtonSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="RadioButtonSample" Height="230" Width="250">
        <StackPanel Margin="10">
                <Label FontWeight="Bold">Are you ready?</Label>
                <RadioButton GroupName="ready">Yes</RadioButton>
                <RadioButton GroupName="ready">No</RadioButton>
                <RadioButton GroupName="ready" IsChecked="True">Maybe</RadioButton>

                <Label FontWeight="Bold">Male or female?</Label>
                <RadioButton GroupName="sex">Male</RadioButton>
                <RadioButton GroupName="sex">Female</RadioButton>
                <RadioButton GroupName="sex" IsChecked="True">Not sure</RadioButton>
        </StackPanel>
</Window>
```

+++

![Two groups of radio buttons using the GroupName property](http://www.wpf-tutorial.com/chapters/basic-controls/images/radiobuttons_group.png "Two groups of radio buttons using the GroupName property")

+++

With the GroupName property set on each of the radio buttons, a selection can now be made for each of the two groups. Without this, only one selection for all six radio buttons would be possible.

---

## Custom content

The RadioButton inherits from the ContentControl class, which means that it can take custom content and display next to it. If you just specify a piece of text, like I did in the example above, WPF will put it inside a TextBlock control and display it, but this is just a shortcut to make things easier for you. You can use any type of control inside of it, as we'll see in the next example:

+++

```C#
<Window x:Class="WpfTutorialSamples.Basic_controls.RadioButtonCustomContentSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="RadioButtonCustomContentSample" Height="150" Width="250">
        <StackPanel Margin="10">
                <Label FontWeight="Bold">Are you ready?</Label>
                <RadioButton>
                        <WrapPanel>
                                <Image Source="/WpfTutorialSamples;component/Images/accept.png" Width="16" Height="16" Margin="0,0,5,0" />
                                <TextBlock Text="Yes" Foreground="Green" />
                        </WrapPanel>
                </RadioButton>
                <RadioButton Margin="0,5">
                        <WrapPanel>
                                <Image Source="/WpfTutorialSamples;component/Images/cancel.png" Width="16" Height="16" Margin="0,0,5,0" />
                                <TextBlock Text="No" Foreground="Red" />
                        </WrapPanel>
                </RadioButton>
                <RadioButton IsChecked="True">
                        <WrapPanel>
                                <Image Source="/WpfTutorialSamples;component/Images/question.png" Width="16" Height="16" Margin="0,0,5,0" />
                                <TextBlock Text="Maybe" Foreground="Gray" />
                        </WrapPanel>
                </RadioButton>
        </StackPanel>
</Window>
```

+++

![Radio buttons with custom content](http://www.wpf-tutorial.com/chapters/basic-controls/images/radiobuttons_custom_content.png "Radio buttons with custom content")

+++

Markup-wise, this example gets a bit heavy, but the concept is pretty simple. For each RadioButton, we have a WrapPanel with an image and a piece of text inside of it. Since we now take control of the text using a TextBlock control, this also allows us to format the text in any way we want to. For this example, I have changed the text color to match the choice. An Image control (read more about those later) is used to display an image for each choice.

+++

Notice how you can click anywhere on the RadioButton, even on the image or the text, to toggle it on, because we have specified it as content of the RadioButton. If you had placed it as a separate panel, next to the RadioButton, the user would have to click directly on the round circle of the RadioButton to activate it, which is less practical.

---

# The PasswordBox control

For editing regular text in WPF we have the TextBox, but what about editing passwords? The functionality is very much the same, but we want WPF to display something else than the actual characters when typing in a password, to shield it from nosy people looking over your shoulder. For this purpose, WPF has the **PasswordBox** control, which is just as easy to use as the TextBox. Allow me to illustrate with an example:

+++

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.PasswordBoxSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="PasswordBoxSample" Height="160" Width="300">
    <StackPanel Margin="10">
        <Label>Text:</Label>
        <TextBox />
        <Label>Password:</Label>
        <PasswordBox />
    </StackPanel>
</Window>
```

+++

![A simple PasswordBox control](http://www.wpf-tutorial.com/chapters/basic-controls/images/passwordbox.png "A simple PasswordBox control")

+++

In the screenshot, I have entered the exact same text into the two text boxes, but in the password version, the characters are replaced with dots. You can actually control which character is used instead of the real characters, using the **PasswordChar** property:

```XML
<PasswordBox PasswordChar="X" />
```

In this case, the character X will be used instead of the dots. 

+++

In case you need to control the length of the password, there's a **MaxLength** property for you:

```XML
<PasswordBox MaxLength="6" />
```

+++

I have used both properties in this updated example:

```XML
<Window x:Class="WpfTutorialSamples.Basic_controls.PasswordBoxSample"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="PasswordBoxSample" Height="160" Width="300">
    <StackPanel Margin="10">
        <Label>Text:</Label>
        <TextBox />
        <Label>Password:</Label>
        <PasswordBox MaxLength="6" PasswordChar="X" />
    </StackPanel>
</Window>
```

+++

![A simple PasswordBox control, with a couple of extra properties set](http://www.wpf-tutorial.com/chapters/basic-controls/images/passwordbox_extra.png "A simple PasswordBox control, with a couple of extra properties set")

Notice how the characters are now X's instead, and that I was only allowed to enter 6 characters in the box.

---

## PasswordBox and binding

When you need to obtain the password from the PasswordBox, you can use the **Password** property from Code-behind. However, for security reasons, the Password property is not implemented as a dependency property, which means that you can't bind to it.

+++

This may or may not be important to you - as already stated, you can still read the password from Code-behind, but for MVVM implementations or if you just love data bindings, a workaround has been developed. You can read much more about it [here](http://blog.functionalfun.net/2008/06/wpf-passwordbox-and-data-binding.html).
