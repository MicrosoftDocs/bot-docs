# Tips and tricks for working with GitHub

## GitHub Desktop

### To make sure your local copy is completely in sync with the remote

Sometimes your local copy of a repo gets so out of sync with the one on GitHub, it's just easier to re-clone the repository.

1. Make sure any local changes you want to keep are stashed outside of the local copy of the repo. If you don't have any work in progress, this may not be necessary.
1. Delete the local root directory for the repo.
1. Open GitHub Desktop and try to set the current repo to the one in question.
1. GitHub Desktop will tell you it can't find the local folder and ask you if you want to re-clone the repo.
1. Re-clone the repo. This may take a minute or two as the app downloads all the files from the remote GitHub repo.
1. If you stashed any changes:
    1. Select the branch you had been working in.
    1. Fetch, and copy your changes back into the local repo.
