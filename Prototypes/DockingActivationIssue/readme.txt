Issues with different versions of the WeifenLuo.WinFormsUI.Docking library where no activation events would fire between floating and docked windows led you to this page:

https://github.com/dockpanelsuite/dockpanelsuite/issues/21

Here is the sample program they provided, it seems to work for a specific library, docpanelsuite (now anyway).

Here is the breakdown as described on the page:
=========================================================================================
We have a layout that updates tool windows whenever a dockpanel form is activated, notifying other dockpanels to clear their controls from the tool windows, which are reused. (Deactivates never seem to come in, so we went with a document declaring focus scheme).

This works properly when switching between docked dockpanels and when switching between floating dockoanels. It does NOT work when switching between docked and floating dockpanels.

On selecting the floating window the first time, we get an activation.
Selecting the docked dockpanelagain, we again get an activation.
Selecting the floating window gets activation.
Selecting the docked dockpanel for the second time, we do NOT get an activation.

repeating thereafter results in no activations in the docked document. Floating dockpanel continues to get activations. The only way to resolve the issue is to either dock the floating window or select another docked dockpanel.

It appears that once we activate the docked dockpanel for the first time, the active pane gets set and thereafter, the framework does not realize that activation events still need to be thrown. We have observed that the suspend focus tracking gets "off" by as much as 3 or 4 through repeatedly selecting one after the other, but it appears random.

So far, I haven't found the solution. :-(