document.addEventListener("DOMContentLoaded", async function () {
    const app = document.getElementById("app");
    const aprUrl = 'http://localhost:5140/api/'

    // Simulated API call with fake team members
    async function fetchTeamMembers() {
        
        return new Promise((resolve) => {
            setTimeout(() => {
                resolve([
                    { id: "123", name: "John Doe" },
                    { id: "124", name: "Jane Smith" },
                    { id: "125", name: "Alex Johnson" }
                ]);
            }, 500); // Simulate network delay
        });
    }

    // Create rating input fields
    function createRatingField(memberId, skill, label) {
        const div = document.createElement("div");
        div.classList.add("mb-2");
        div.innerHTML = `
            <label class="form-label">${label}:</label>
            <select class="form-select" name="${memberId}-${skill}" required>
                <option value="1">1 - Poor</option>
                <option value="2">2 - Fair</option>
                <option value="3">3 - Satisfactory</option>
                <option value="4">4 - Very Good</option>
                <option value="5">5 - Excellent</option>
            </select>
        `;
        return div;
    }

    // Create text input fields
    function createTextField(memberId, field, label) {
        const div = document.createElement("div");
        div.classList.add("mb-2");
        div.innerHTML = `
            <label class="form-label">${label}:</label>
            <textarea class="form-control" name="${memberId}-${field}" rows="2" required></textarea>
        `;
        return div;
    }

    // Generate form for each team member (including self)
    async function generateFeedbackForm() {
        const teamMembers = await fetchTeamMembers();
        teamMembers.push({ id: "self", name: "Yourself" });

        const form = document.createElement("form");
        form.id = "feedbackForm";
        form.classList.add("container", "mt-4");

        teamMembers.forEach(member => {
            const memberDiv = document.createElement("div");
            memberDiv.classList.add("card", "mb-4");
            memberDiv.innerHTML = `
                <div class="card-header">
                    <h5 class="mb-0">Feedback for ${member.name}</h5>
                </div>
                <div class="card-body">
                </div>
            `;

            const cardBody = memberDiv.querySelector(".card-body");

            const skills = [
                { key: "technical", label: "Technical" },
                { key: "analytical", label: "Analytical" },
                { key: "communication", label: "Communication" },
                { key: "participation", label: "Participation" },
                { key: "performance", label: "Overall Performance" }
            ];

            skills.forEach(skill => {
                cardBody.appendChild(createRatingField(member.id, skill.key, skill.label));
            });

            cardBody.appendChild(createTextField(member.id, "strengths", "Strengths"));
            cardBody.appendChild(createTextField(member.id, "improvements", "Improvements"));
            cardBody.appendChild(createTextField(member.id, "comments", "Additional Comments"));

            form.appendChild(memberDiv);
        });

        // Submit button
        const submitButton = document.createElement("button");
        submitButton.type = "submit";
        submitButton.classList.add("btn", "btn-primary", "w-100");
        submitButton.textContent = "Submit Feedback";
        form.appendChild(submitButton);

        app.appendChild(form);

        // Handle form submission
        form.addEventListener("submit", function (event) {
            event.preventDefault();
            submitFeedback(teamMembers);
        });
    }

    // Collect data and send to API
    function submitFeedback(teamMembers) {
        const formData = new FormData(document.getElementById("feedbackForm"));
        const feedbackData = [];

        teamMembers.forEach(member => {
            const feedback = {
                memberId: member.id,
                ratings: {
                    technical: formData.get(`${member.id}-technical`),
                    analytical: formData.get(`${member.id}-analytical`),
                    communication: formData.get(`${member.id}-communication`),
                    participation: formData.get(`${member.id}-participation`),
                    performance: formData.get(`${member.id}-performance`)
                },
                strengths: formData.get(`${member.id}-strengths`),
                improvements: formData.get(`${member.id}-improvements`),
                comments: formData.get(`${member.id}-comments`)
            };
            feedbackData.push(feedback);
        });

        console.log("Submitting feedback:", feedbackData);

        // fetch('/api/submit-feedback', {
        //     method: 'POST',
        //     headers: { 'Content-Type': 'application/json' },
        //     body: JSON.stringify(feedbackData)
        // })
        // .then(response => response.json())
        // .then(data => {
        //     console.log("Feedback submitted successfully:", data);
        //     alert("Feedback submitted!");
        //     document.getElementById("feedbackForm").reset();
        // })
        // .catch(error => {
        //     console.error("Error submitting feedback:", error);
        //     alert("Error submitting feedback. Please try again.");
        // });
    }

    // Initialize feedback form
    generateFeedbackForm();
});
