using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.Infrastructure.TopicSections;
using CygSoft.CodeCat.Plugins.TopicSections.Infrastructure;
using System;
using WebReferencesTopicSectionPlugin;

namespace CygSoft.CodeCat.UI.WinForms.Documents
{
    public static class TopicSectionControlFactory
    {
        public static ITopicSectionBaseControl Create(ITopicSection topicSection, IImageResources imageResources, IFile workItem, 
            IAppFacade application, EventHandler modifiedEventHandler)
        {
            ITopicSectionBaseControl topicSectionControl = null;

            if (topicSection is IWebReferencesTopicSection)
                topicSectionControl = new WebReferencesTopicSectionControl(application, imageResources, workItem as ITopicDocument, topicSection as IWebReferencesTopicSection);

            else
                return null;

            if (topicSectionControl != null)
                topicSectionControl.Modified += modifiedEventHandler;

            return topicSectionControl;
        }
    }
}
