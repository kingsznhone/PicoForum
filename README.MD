# Pico Forum

## What is this

Pico Forum is an ultra-lightweight forum framework built on ASP.NET Core Blazor.

[Demo Host](https://pico.zangai.family/) (On my ubuntu server)

Unlike common open-source forum projects that are developed using older technologies like PHP, the codebases of these projects have accumulated over the years, reaching an unimaginable level of complexity, becoming bloated and fragile.

A forum doesn't necessarily have to be extensive; it can be compact and lightweight. This is where Pico Forum comes in.

True to its name, it's a micro forum that can be deployed on your home computer.

During the HackTogether event, thanks to the powerful capabilities of .NET 8, I took on the challenge of Blazor and completed the demo in just about a week on my own. 

What Excited me the most was the seamless integration of Razor Pages and Bootstrap, freeing developers from the historical struggles with JavaScript and CSS in the mud. 

The Server Interactive Razor Page eliminates the presence of MVC in the entire project, creating a unique sensation that requires abandoning traditional website development thinking.

Pico Forum implements the fundamental features expected of a forum, including posting, replying, deleting posts, and banning users.

I am not good at designing frontend pages, so the layout may appear a bit ugly. 

Contribution is welcome.

## Preview


<img src="https://github.com/kingsznhone/PicoForum/blob/main/README/home.jpg" width="852" height="480" alt="Home Page"/><br/>

<img src="https://github.com/kingsznhone/PicoForum/blob/main/README/section.jpg" width="852" height="480" alt="Section Page"/><br/>

<img src="https://github.com/kingsznhone/PicoForum/blob/main/README/profile.jpg" width="852" height="480" alt="Profile Page"/><br/>

<img src="https://github.com/kingsznhone/PicoForum/blob/main/README/post.jpg" width="852" height="480" alt="Post Page"/><br/>

<img src="https://github.com/kingsznhone/PicoForum/blob/main/README/controlpanel.jpg" width="852" height="480" alt="Control Panel Page"/><br/>

## Usage

1. download ASP.NET Core Runtime 8.0.0

2. Download release.

3. Edit ```appsetting.json``` for listen url.

4. ```$ dotnet ./picoforum.dll``` on Linux <br> double click on ```picoforum.exe``` on Windows

5. Input initial password for admin@picoforum.com(Super Admin) in console.


## Key Features:

1. Use an SQLite database, eliminating the need for a heavy database service installation.

2. Uses JSON-formatted extensible persistence configuration files that can be modified either in the web-based administration panel or through the operating system's file manager.

3. Use [TinyMCE](https://www.tiny.cloud/) WYSIWYG editor. RichText Fully Supported (See Below)

4. Low coupling with the system environment; backup only requires copying the directory.

5. Cross-platform on Windows and Linux. (Not fully tested yet)

6. No phone number, no email validation. Reg & Go like internet 90's.

7. I guess the performance should be pretty cool with .NET8 , compare with those PHP old buddy.

## How to Configure [TinyMCE](https://www.tiny.cloud/)

1. Registry account.

2. go to [dashboard](https://www.tiny.cloud/my-account/integrate/)

3. Copy Api Key to ```appsetting.json```

4. Add your host domain in  [dashboard](https://www.tiny.cloud/my-account/domains/)

I think Blazor should have more useful controls like this. 

## Change Log

### v0.0.0.2

#### 2023.12.06

- Add avatar upload. with safety check.

- Add Jpeg compress quality option.

- Add max avatar upload size option.

- Use a better config option to control ip/port binding.

<img src="https://github.com/kingsznhone/PicoForum/blob/main/README/avatar.jpg" alt="profile avatar"/><br/>

### v0.0.0.1  

#### 2023.12.04

The Pico Forum is tiny, so the version number is also tiny.

## Roadmap

### Short Term

1. Add more infomation on Razor Component. Such as Total View/Reply.

2. Add more field in user table. Like signature something.

3. Search for posts.
   
### Long Term

Open an issue to share your opion.
