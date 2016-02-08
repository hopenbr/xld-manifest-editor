// Copyright (c) 2015, XebiaLabs B.V., All rights reserved.
//
//
// The Manifest Editor for XL Deploy is licensed under the terms of the GPLv2
// <http://www.gnu.org/licenses/old-licenses/gpl-2.0.html>, like most XebiaLabs Libraries.
// There are special exceptions to the terms and conditions of the GPLv2 as it is applied to
// this software, see the FLOSS License Exception
// <https://github.com/xebialabs-community/xld-manifest-editor/blob/master/LICENSE>.
//
// This program is free software; you can redistribute it and/or modify it under the terms
// of the GNU General Public License as published by the Free Software Foundation; version 2
// of the License.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with this
// program; if not, write to the Free Software Foundation, Inc., 51 Franklin St, Fifth
// Floor, Boston, MA 02110-1301  USA
//
using XebiaLabs.Deployit.Client.Http;
using XebiaLabs.Deployit.Client.UDM;

namespace XebiaLabs.Deployit.Client
{
    internal sealed class TaskService : ServiceBase, ITaskService
    {
        public TaskService(DeployitServer server)
            : base(server,"task")
        {
        }

        public TaskInfo GetTaskInfo(string taskId, bool showDetails)
        {
			var	command = BuildCommand(showDetails?"{0}/step":"{0}", taskId);
            return ExecuteHttp<TaskInfo, UDMHttpContent<TaskInfo>, string, StringHttpContent>(new GetHttpResponseProvider(), command);
        }

		private void SendTaskAction(string taskId,string action)
		{
			var command = BuildCommand("{0}/{1}", taskId,action);
		    ExecuteHttp<object, NoHttpContent, string, StringHttpContent>(
		        new PostHttpResponseProvider(new EmptyPostInputContent()), command,
		        DeployitServer.CheckIfStatusCodeIsOKorNotContent);
		}

		public void Start(string taskId)
        {
			SendTaskAction(taskId,"start");
        }

        public void Archive(string taskId)
        {
			SendTaskAction(taskId, "archive");
        }

        public void Stop(string taskId)
        {
			SendTaskAction(taskId, "stop");
        }

        public void Cancel(string taskId)
        {
			SendTaskAction(taskId, "cancel");
        }
    }
}
