# Technical content channel guidance

GitHub is a relatively easy way (once you get over the Git hump) to author and publish technical content. But we need to ensure that content stays within the boundaries of technical documentation - there are other channels for other types of information.

## Technical content that belongs in the bot-framework-docs-pr repository

The following types of content are authored in bot-framework-docs-pr and published to [Azure documentation](https://learn.microsoft.com/azure).

**Technical articles about using the product** belong in the bot-framework-docs and bot-framework-docs-pr repositories for publication to Microsoft Learn. They should contain conceptual or procedural information required to understand and use the product. The technical content channel is for technical content showing people **how** to do something. You can talk about the "what" and "why" to help customers understand intent, but the articles should focus on the actual content telling people how to do the task or complete the scenario.

Specific guidance exists for certain types of content, such as customer stories and service limits.

* **Case studies/customer stories**: In principle, customer stories/case studies can live either on the [customer stories site](https://customers.microsoft.com/) or in the technical documentation repository, which publishes to [Azure documentation](https://learn.microsoft.com/azure). The difference is audience and technical depth. The customer stories site addresses BDM and TDM audiences, and the content is not about technical deployment or operational details. The technical content channel addresses evaluators, implementers, and users and goes into technical depth appropriate for new to advanced users. Customer stories require approval, see [What can I publish on Microsoft Learn?](https://review.learn.microsoft.com/help/contribute/contribute-get-started-channel-guidance?branch=main) for more info.

* **Service limits**: Every service where subscription and service limits, quotas, and constraints apply must have an include file that documents the most important limits, quotas, or constraints. The include must be added to the all-up [Bot Framework subscription and service limits, quotas, and constraints](https://learn.microsoft.com/azure/azure-subscription-service-limits) article. Optionally, a service-specific limits article may also be published to the service-specific content; the include must be reused in that article.

* **Top issue troubleshooting content**: Most technical troubleshooting content should be published as KB articles at [Microsoft Support](https://support.microsoft.com/). Per agreement with CSS and Apex management, each Bot Framework service may also publish a limited set of two types of troubleshooting content with their technical documentation: up to 10 top issue troubleshooting articles per service, and up to 10 troubleshooting index articles that summarize the symptoms, causes, quick steps, and links to other content (KB articles or documentation) for top call drivers.

## Technical content that does not belong in the bot-framework-docs-pr repository

The following types of content are delivered in other Bot Framework or Microsoft content channels; in some cases, certain types of content are not part of our content strategy.

* **Blog posts**: Blog posts are typically written in the first person voice and are generally related to announcements and promotions. This sort of content typically belongs on the Bot Framework blog. Deeply technical or procedural content does not go on the blog.

* **Code and project samples**: Code and project samples go in the samples repositories and are featured in the sample gallery.

* **Community spotlight/community resources**: Articles featuring community projects. The repo is for technical content about how to use the product from the Microsoft perspective, not about how people are using the product. That's marketing or possibly blog content. Or, let the community tell it's own story in the places that community likes best!

* **Compliance**: Industry standards and compliance information for Bot Framework services must be posted to https://www.microsoft.com/TrustCenter/Compliance?service=Azure#Icons. This includes certification for standards such as ISO, country-specific and government certifications, banking, health, or other certifications.

* **Downloadable files**: Technical documents should be delivered as articles, not downloads. Do not create downloadable PDFs of content from the technical content repository. Other downloadable things should go to the Download Center.

* **Feedback**:
  * Soliciting feedback via email addresses. The approved feedback paths for Bot Framework content include the feedback link that appears in the site footer, the satisfaction rating and verbatim control, direct article contributions through GitHub pull requests, and the UserVoice site. Please don't add to this plethora of channels by asking people to send feedback via email.
  * Articles asking for feedback are not technical content and should not be published. There are plenty of feedback methods available on the site.

* **Future product plans**: Do not publish statements about future product plans in technical documentation. Technical documentation should describe only what is possible in the released product.

* **Learning paths**: Learning paths are delivered at [Microsoft Learn training](https://learn.microsoft.com/training/). See the [Microsoft Learn Contributor Guide](https://review.learn.microsoft.com/help/learn/?branch=main) for instructions.

* **Legal terms**: There are all-up Bot Framework legal terms: [Microsoft Azure Legal Information](https://azure.microsoft.com/support/legal/).

* **Marketing content**: Content that provides a high-level feature/benefit description or that just lists at a high level the capabilities of a service is probably marketing content. It belongs in marketing areas of the site. To publish marketing content, file a work request for azure.microsoft.com.

* **Placeholder articles for future content**: Do not publish "coming soon" articles as placeholders for future content. We only publish actual technical content articles that contain relevant, useful technical content.

* **Pricing information**: You can talk about the impact technical choices have on cost in a general way, but do not quote specific pricing details in technical articles. Instead, provide a link to the pricing page for the service you're talking about.

* **Pointer articles to downloads**: Instead of pointing small pages that contain nothing but a link to a download, just link to the download from the relevant technical content.

* **Privacy information**: There is an all-up privacy policy for Microsoft Online Services that covers all of Azure. Privacy information specific to a service should be presented as technical content, not "privacy statements". See [Microsoft Azure Legal Information](https://azure.microsoft.com/support/legal/).

* **Redirect articles**:  Do not republish an article so that the content of the article is a link to the replacement content. Convert the article into an actual redirect so the user doesn't have to click to go the replacement content.

* **Reference content**: Managed reference, REST APIs, PowerShell cmdlet help, schema reference, and error reference content is published to Microsoft Learn, but not through this repository.

* **Regions**: In technical articles, do not discuss, describe, or explain the regions in which a feature, product, service, or procedure applies or is available. Feature and service availability by region is documented only on the following ACOM page: [Products available by region](https://azure.microsoft.com/regions/services/). Provide a link to the ACOM page.

  You can use specific regions in examples when you're describing procedures or tools where a customer has to work across regions. For example, this sentence is OK:

  > The following screenshot shows two pings from two different region client machines, one in the East Asia region and one in the West US.

* **Release notes**: Unless it's an SDK article or a StorSimple article for a hardware update, this sort of information should just be embedded in the relevant technical content or included in the [service updates channel](https://azure.microsoft.com/updates/).

* **SLA**: Do not mention any specifics about SLAs in the technical documents. Always point to the SLA page for the service. The SLA index page is here: [Service-level agreements](https://azure.microsoft.com/support/legal/sla/). Example: "For information about the SLA, see the [Bot Framework service level agreements](https://azure.microsoft.com/support/legal/sla/) page."

* **Troubleshooting content of most types**: Although each service may include up to 10 troubleshooting articles and index files as described above, most formal troubleshooting and support content should be published as KB articles on [Microsoft Support](https://support.microsoft.com).

* **What's new in a release or service**:  Lists or descriptions of what is new in a release or service go to the [Service Updates channel](https://azure.microsoft.com/updates/).
