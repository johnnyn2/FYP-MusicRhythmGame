# MusicRhythmGame
--------------------
## Project Setup: Install UnityHub and Unity (https://unity3d.com/get-unity/download)
### Step 1: Install UnityHub
### Step 2: Install Unity
1. Open <b>UnityHub</b> and click "<b>安裝</b>". Then, click "<b>新增</b>"
2. Choose Unity version <b>2019.2.5f1</b>
3. Choose to install "<b>WebGL</b>", "<b>Documentation</b>", "<b>繁體中文</b>"
--------------------
## Project Setup: How to use git with Unity
### Step 1. Clone the project
1. Open cmd

2. The default directory should be <b>C:\Users\\"Your name"\ </b>

3. You may clone the project in any directory. I suggest to clone in <b>C:\Users\\"Your name"\Documents</b>. So, run "<b>cd Documents</b>". And you will be proceed to <b>C:\Users\\"Your name"\Documents </b>

4. Press the green "<b>Clone or download </b>" button above. The copy the project url in SSH mode or HTTPS mode. I use Clone with HTTPS since I don't have SSH keys in my github account. If you wish to use SSH, please reference to this article https://help.github.com/en/enterprise/2.15/user/articles/generating-a-new-ssh-key-and-adding-it-to-the-ssh-agent

5. Back to the cmd, run "<b>git clone https://github.com/johnnyn2/MusicRhythmGame.git </b>"

6. Run "<b>cd MusicRhythmGame </b>"

7. Current directory should be "<b>C:\Users\\"Your name"\Documents\MusicRhythmGame</b>". Run "<b>git status </b>" and it should state "<b>Your branch is up to date with 'origin/master'</b>". "git status" command would state what is the current branch you are in and whether there changes in the current branch made by you or not.
--------------------
### Step 2. Checkout a new branch 
1. You should be in <b>master</b> branch now. If you are not sure, run "<b>git status</b>" to check it. If you are not in master branch, run "<b>git checkout master</b>". If you are in master branch,  run "<b>git checkout -b dev_YOURNAME</b>". E.g. I would run "<b>git checkout -b dev_sam</b>". I suggest to use "<b>dev_YOURNAME</b>" as branch name. It is more clear that can indicate the current branch belongs to whom. Command "<b>git checkout -b</b>" let you fork a new branch from the current branch you are currently locating at and then check out to that newly created branch. 

2. Run "git status". It should indicates that you are in "<b>dev_YOURNAME</b>".

3. Run "<b>git branch --set-upstream-to=origin/master BRANCHNAME</b>". E.g. I would run "<b>git branch --set-upstream-to=origin/master dev_sam</b>". This command let the newly created branch keeps track of the changes in master branch. In future, if there is any change in master, simply run "<b>git pull</b>" would do the work.

4. Then, run "<b>git pull</b>" to make sure the branch is up-to-date with master. 

5. Then, run "<b>git push origin BRANCHNAME</b>".E.g. I would run "<b>git push origin dev_sam</b>". After pushing the branch, you will see your branch under the master branch in this github repository webpage. 
--------------------
### Step 3. Work on a branch
1. Now, you are ready to work on the new branch. You can open the project folder by UnityHub. The location of the project should be "<b>C:\Users\\"Your name"\Documents\MusicRhythmGame\MusicRhythmGame</b>". Once you have made any changes and want to commit and push those changes to the origin. You should first run "<b>git status</b>". It would shows all the changed files. Then, run "<b>git add -A</b>". This is to add the changes to stage. Then, run "<b>git commit -m "YOUR_COMMIT_MESSAGE"</b>". This is to add a commit message to the changes(tell what you have done in this commit). Then, run "<b>git pull</b>" to ensure your code piece is up-to-date with the code in master. Then, run "<b>git push origin BRANCHNAME</b>". E.g. I would run "<b>git push origin dev_sam</b>". Then, you should be able to see the changes in the github repository. 

2. Every important!!! Remember to pull code before push code. Otherwise, the branch may be fast-forwarded !!! And no longer be able to keep track of the upstream branch(master). In this case, you have to run another set of commands to fix the fast-forward problem.  
--------------------
### Step 4. Create pull request when some feature is ready
1. Create pull request in the repository webpage once you have completed some feature(s). Pull request would compare the changes between the base branch and compare branch. E.g. base: master, compare: dev_sam. Once the pull request is accepted. The changes in your branch would be merged to master branch.
--------------------
### Reamrks:
1. For more git command, please visit https://github.com/joshnh/Git-Commands
--------------------
#### How to set Visual Studio Code as prefered text editor in Unity?
1. Open the project via Unity Hub
2. Click "Edit" -> "Preferences..." -> "External Script Editer" -> "Visual Studio Code"
3. Close the Window
4. Click "Assets" -> "Open C# Project"
5. VS Code should open your project
--------------------
#### Visual Studio Code Intellisense and Recommended Extensions
1. VS Code is the best editor for editing Unity scripts as it provides a lot of extensions that help us for development. Some recommended extensions are:
- <b>C#</b>
- <b>Unity Code Snippets</b>
- <b>Unity Tools</b>
1. In order to setup intellisense for coding Unity Scripts in VS Code, please download all the extension above.
2. After downloading C# extension, it should prompt you to install <b>.Net core sdk</b> if you havn not installed it yet. Just install the latest <b>.Net core sdk</b> (https://dotnet.microsoft.com/download/dotnet-core) will do the work.
3. Then, OmniSharp server will start for our project and it will fail in loading <b>Assembly-CSharp.csproj</b> file (in the "Ouput" section of VS Code terminal).
4. Then, you need to install <b>.Net Framework developer pack version 4.7.1</b> (https://dotnet.microsoft.com/download/dotnet-framework)
5. OmniSharp server should be able to load Assembly-CSharp.csproj now. Open any .cs file and try whether there is intellisense or not. For example, type "gameO" and it should suggest you "gameObject" variable
--------------------
