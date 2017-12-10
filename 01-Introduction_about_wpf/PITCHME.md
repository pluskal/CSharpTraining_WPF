# What is WPF?

WPF, which stands for Windows Presentation Foundation, is Microsoft's latest approach to a GUI framework, used with the .NET framework.

But what IS a GUI framework? GUI stands for Graphical User Interface, and you're probably looking at one right now. Windows has a GUI for working with your computer, and the browser that you're likely reading this document in has a GUI that allows you to surf the web.

+++

A GUI framework allows you to create an application with a wide range of GUI elements, like labels, textboxes and other well known elements. Without a GUI framework you would have to draw these elements manually and handle all of the user interaction scenarios like text and mouse input. This is a LOT of work, so instead, most developers will use a GUI framework which will do all the basic work and allow the developers to focus on making great applications.

+++

There are a lot of GUI frameworks out there, but for .NET developers, the most interesting ones are currently WinForms and WPF. WPF is the newest, but Microsoft is still maintaining and supporting WinForms. As you will see in the next chapter, there are quite a few differences between the two frameworks, but their purpose is the same: To make it easy to create applications with a great GUI.

In the next chapter, we will look at the differences between WinForms and WPF.

---

# WPF vs. WinForms


In the previous chapter, we talked about what WPF is and a little bit about WinForms. In this chapter, I will try to compare the two, because while they do serve the same purpose, there is a LOT of differences between them. If you have never worked with WinForms before, and especially if WPF is your very first GUI framework, you may skip this chapter, but if you're interested in the differences then read on.

+++

The single most important difference between WinForms and WPF is the fact that while WinForms is simply a layer on top of the standard Windows controls (e.g. a TextBox), WPF is built from scratch and doesn't rely on standard Windows controls in almost all situations. This might seem like a subtle difference, but it really isn't, which you will definitely notice if you have ever worked with a framework that depends on Win32/WinAPI.

+++

A great example of this is a button with an image and text on it. This is not a standard Windows control, so WinForms doesn't offer you this possibility out of the box. Instead you will have to draw the image yourself, implement your own button that supports images or use a 3rd party control. With WPF, a button can contain anything because it's essentially a border with content and various states (e.g. untouched, hovered, pressed). The WPF button is "look-less", as are most other WPF controls, which means that it can contain a range of other controls inside of it. You want a button with an image and some text? 

+++

Just put an Image and a TextBlock control inside of the button and you're done! You simply don’t get this kind of flexibility out of the standard WinForms controls, which is why there's a big market for rather simple implementations of controls like buttons with images and so on.

+++

The drawback to this flexibility is that sometimes you will have to work harder to achieve something that was very easy with WinForms, because it was created for just the scenario you need it for. At least that's how it feels in the beginning, where you find yourself creating templates to make a ListView with an image and some nicely aligned text, something that the WinForms ListViewItem does in a single line of code.

+++

This was just one difference, but as you work with WPF, you will realize that it is in fact the underlying reason for many of the other differences - WPF is simply just doing things in its own way, for better and for worse. You're no longer constrained to doing things the Windows way, but to get this kind of flexibility, you pay with a little more work when you're really just looking to do things the Windows way.

+++

The following is a completely subjective list of the key advantages for WPF and WinForms. It should give you a better idea of what you're going into.

+++

## WPF advantages

*   It's newer and thereby more in tune with current standards
*   Microsoft is using it for a lot of new applications, e.g. Visual Studio
*   It's more flexible, so you can do more things without having to write or buy new controls
*   When you do need to use 3rd party controls, the developers of these controls will likely be more focused on WPF because it's newer
*   XAML makes it easy to create and edit your GUI, and allows the work to be split between a designer (XAML) and a programmer (C#, VB.NET etc.)
*   Databinding, which allows you to get a more clean separation of data and layout
*   Uses hardware acceleration for drawing the GUI, for better performance
*   It allows you to make user interfaces for both Windows applications and web applications (Silverlight/XBAP)

+++

## WinForms advantages

*   It's older and thereby more tried and tested
*   There are already a lot of 3rd party controls that you can buy or get for free
*   The designer in Visual Studio is still, as of writing, better for WinForms than for WPF, where you will have to do more of the work yourself with WPF