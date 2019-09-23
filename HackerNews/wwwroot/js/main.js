var app = app || {
    addStory: function (id) {
        let self = this;
        let storyContainer = document.getElementById('stories-container');

        let story = document.createElement('div');
        story.setAttribute('id', `story-${id}`)
        story.innerHTML = 'loading story...';
        storyContainer.appendChild(story);

        self.getStoryById(id);
    },

    updateStory: function (id, title, author, url) {
        let self = this;

        let story = document.getElementById(`story-${id}`);
        story.innerHTML = self.template(title, author, url);
    },

    clearStories: function () {
        let element = document.getElementById("stories-container");
        element.innerHTML = '';
    },

    template: (title, author, url) => {
        return `<div><h2><a href="${url}"> ${title}</a><h2></div><div>by ${author}</div>`;
    },

    getStoryById: function (id) {
        let self = this;
        let success = function (e) {
            if (e.target.response) {
                const story = JSON.parse(e.target.response);
                self.updateStory(story.id, story.title, story.by, story.url);
            }
        };
        let error = function (e) {
            console.log('request failed. Ex:');
            console.log(e);
        };

        service.request('GET', `/api/news/${id}`).then(success, error);
    },

    getNewStories: function () {
        let self = this;
        let success = function (e) {
            //pre-render the div's while loading
            if (e.target.response) {
                const storyIdArray = JSON.parse(e.target.response);
                storyIdArray.forEach(id => {
                    self.addStory(id);
                    self.story
                });


            }
        };
        let error = function (e) {
            console.log('request failed. Ex:');
            console.log(e);
        };

        service.request('GET', '/api/news').then(success, error);
    },
}

window.addEventListener('load', (event) => {
    app.getNewStories();
});